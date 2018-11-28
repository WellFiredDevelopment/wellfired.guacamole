using System.IO;
using WellFired.Guacamole.DataStorage.Exceptions;
using WellFired.Guacamole.DataStorage.Synchronization;

namespace WellFired.Guacamole.DataStorage.Types
{
	/// <summary>
	/// Store textual data in a key/value fashion, key being the file and value the data saved inside. This file is saved a the path
	/// indicated in the constructor. The class is thread safe, therefore different instances of <see cref="FileStorageService"/>
	/// can read and write at the same location on different threads.
	/// </summary>
	public class FileStorageService : IDataStorageService
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
		
		#endregion
		
		public FileStorageService(string savingFolder)
		{
			Location = savingFolder;
			Directory.CreateDirectory(Location);
		}

		/// <inheritdoc />
		public string Location { get; }

		/// <inheritdoc />
		public string Read(string key)
		{
			SharedThreadLock.EnterReadLock(Location + key);
			try
			{
				return File.ReadAllText($"{Location}/{key}");
			}
			finally
			{
				SharedThreadLock.ExitReadLock(Location + key);
			}
		}

		/// <summary>
		/// Write the file key inside <see cref="Location"/>. If some directories are missing in the path, they are created.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			SharedThreadLock.EnterWriteLock(Location + key);
			try
			{
				File.WriteAllText($"{Location}/{key}", data);
			}
			finally
			{
				SharedThreadLock.ExitWriteLock(Location + key);
			}
		}

		/// <inheritdoc />
		public void Delete(string key)
		{
			SharedThreadLock.EnterWriteLock(Location + key);
			try
			{
				File.Delete($"{Location}/{key}");
			}
			finally
			{
				SharedThreadLock.ExitWriteLock(Location + key);
			}
		}

		/// <inheritdoc />
		public bool Exists(string key)
		{
			SharedThreadLock.EnterReadLock(Location + key);
			
			try
			{
				return File.Exists($"{Location}/{key}");
			}
			finally
			{
				SharedThreadLock.ExitReadLock(Location + key);
			}
		}
	}
}