using System.IO;

namespace WellFired.Guacamole.DataStorage.Storages
{
	/// <summary>
	/// Store textual data in a key/value fashion, key being the file and value the data saved inside. This file is saved a the path
	/// indicated in the constructor. The class is thread safe, therefore different instances of <see cref="FileStorageSynchronizer"/>
	/// can read and write at the same location on different threads.
	/// </summary>
	public class FileStorageService : IDataStorageService
	{
		private static readonly FileStorageSynchronizer FileStorageSynchronizer = new FileStorageSynchronizer();
		
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
			FileStorageSynchronizer.EnterReadLock(Location + key);
			try
			{
				return File.ReadAllText($"{Location}/{key}");
			}
			finally
			{
				FileStorageSynchronizer.ExitReadLock(Location + key);
			}
		}

		/// <summary>
		/// Write the file key inside <see cref="Location"/>. If some directories are missing in the path, they are created.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			FileStorageSynchronizer.EnterWriteLock(Location + key);
			try
			{
				File.WriteAllText($"{Location}/{key}", data);
			}
			finally
			{
				FileStorageSynchronizer.ExitWriteLock(Location + key);
			}
		}

		/// <inheritdoc />
		public void Delete(string key)
		{
			FileStorageSynchronizer.EnterReadLock(Location + key);
			try
			{
				File.Delete($"{Location}/{key}");
			}
			finally
			{
				FileStorageSynchronizer.ExitReadLock(Location + key);
			}
		}

		/// <inheritdoc />
		public bool Exists(string key)
		{
			FileStorageSynchronizer.EnterReadLock(Location + key);
			
			try
			{
				return File.Exists($"{Location}/{key}");
			}
			finally
			{
				FileStorageSynchronizer.ExitReadLock(Location + key);
			}
		}
	}
}