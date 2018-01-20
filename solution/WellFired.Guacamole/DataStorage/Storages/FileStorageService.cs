using System;
using System.IO;

namespace WellFired.Guacamole.DataStorage.Storages
{
	/// <summary>
	/// Store textual data in a key/value fashion, key being the file and value the data saved inside. This file is saved a the path
	/// indicated in the constructor.
	/// </summary>
	public class FileStorageService : IDataStorageService
	{
		public FileStorageService(string savingFolder) => Location = savingFolder;

		/// <inheritdoc />
		public string Location { get; }

		/// <inheritdoc />
		public string Read(string key) => File.ReadAllText($"{Location}/{key}");

		/// <summary>
		/// Write the file key inside <see cref="Location"/>. If some directories are missing in the path, they are created.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			Directory.CreateDirectory(Path.GetDirectoryName($"{Location}/{key}") ?? throw new Exception($"The path {Location}/{key} is invalid."));
			File.WriteAllText($"{Location}/{key}", data);
		}

		/// <inheritdoc />
		public void Delete(string key) => File.Delete($"{Location}/{key}");

		/// <inheritdoc />
		public bool Exists(string key) => File.Exists($"{Location}/{key}");
	}
}