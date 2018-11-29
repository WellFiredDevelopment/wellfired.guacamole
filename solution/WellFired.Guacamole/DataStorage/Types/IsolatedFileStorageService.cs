using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using WellFired.Guacamole.DataStorage.Exceptions;
using WellFired.Guacamole.DataStorage.Synchronization;

namespace WellFired.Guacamole.DataStorage.Types
{
	/// <inheritdoc />
	/// <summary>
	/// Store textual data in a key/value fashion, key being the file and value the data saved inside. This file is saved a the path
	/// indicated in the constructor. The class is thread safe, therefore different instances of <see cref="T:WellFired.Guacamole.DataStorage.Types.IsolatedFileStorageService" />
	/// can read and write at the same location on different threads.
	/// </summary>
	public class IsolatedFileStorageService : IDataStorageService
	{
		#region Thread Synchronization
		
		public static void InitializeSharedThreadLock(IKeyBasedReadWriteLock readWriteLock, bool forceReinitialization = false)
		{
			if (_sharedThreadLock != null && !forceReinitialization)
			{
				throw new AlreadyInitializeException();
			}
			
			_sharedThreadLock = new ThreadSynchronizer(readWriteLock);
		}

		private static ThreadSynchronizer SharedThreadLock => _sharedThreadLock ?? (_sharedThreadLock = new ThreadSynchronizer(new KeyBasedReadWriteLock()));
		private static ThreadSynchronizer _sharedThreadLock;
		private readonly IsolatedStorageFile _isoStore;

		#endregion
		
		public IsolatedFileStorageService(string subFolder = "data")
		{
			if(string.IsNullOrEmpty(subFolder))
				throw new ArgumentNullException(nameof(subFolder) + " should not be null, empty or whitespace");
			
			Location = subFolder;
			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
			_isoStore.CreateDirectory(Location);
		}

		/// <inheritdoc />
		public string Location { get; }

		/// <inheritdoc />
		public string Read(string key)
		{
			var storageKey = Path.Combine(Location, key);
			SharedThreadLock.EnterReadLock(storageKey);
			try
			{
				using (var isoStream = new IsolatedStorageFileStream(storageKey, FileMode.Open, _isoStore))
				using (var reader = new StreamReader(isoStream))
					return reader.ReadToEnd();
			}
			finally
			{
				SharedThreadLock.ExitReadLock(storageKey);
			}
		}

		/// <summary>
		/// Write the file key inside <see cref="Location"/>. If some directories are missing in the path, they are created.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			var storageKey = Path.Combine(Location, key);
			
			if(Exists(key))
				Delete(key);
			
			SharedThreadLock.EnterWriteLock(storageKey);
			try
			{
				using (var isoStream = new IsolatedStorageFileStream(storageKey, FileMode.CreateNew, _isoStore))
				using (var writer = new StreamWriter(isoStream))
					writer.Write(data);
			}
			finally
			{
				SharedThreadLock.ExitWriteLock(storageKey);
			}
		}

		/// <inheritdoc />
		public void Delete(string key)
		{
			var storageKey = Path.Combine(Location, key);
			SharedThreadLock.EnterWriteLock(storageKey);
			try
			{
				_isoStore.DeleteFile(storageKey);
			}
			finally
			{
				SharedThreadLock.ExitWriteLock(storageKey);
			}
		}

		/// <inheritdoc />
		public bool Exists(string key)
		{
			var storageKey = Path.Combine(Location, key);
			SharedThreadLock.EnterReadLock(storageKey);
			
			try
			{
				return _isoStore
					.GetFileNames($"{Location}/*")
					.Any(o => o == key);
			}
			finally
			{
				SharedThreadLock.ExitReadLock(storageKey);
			}
		}

		public void Clear()
		{
			_isoStore.Remove();
		}
	}
}