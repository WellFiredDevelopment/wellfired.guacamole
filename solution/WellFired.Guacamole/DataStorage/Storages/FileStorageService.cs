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
		private readonly string _savingFolder;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="savingFolder">Path where are saved the key files</param>
		public FileStorageService(string savingFolder) => _savingFolder = savingFolder;

		/// <inheritdoc />
		public string Read(string key) => File.ReadAllText($"{_savingFolder}/{key}");

		/// <summary>
		/// Write the file key inside <see cref="_savingFolder"/>. If some directories are missing in the path, they are created.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			Directory.CreateDirectory(Path.GetDirectoryName($"{_savingFolder}/{key}") ?? throw new Exception($"The path {_savingFolder}/{key} is invalid."));
			File.WriteAllText($"{_savingFolder}/{key}", data);
		}

		/// <inheritdoc />
		public void Delete(string key) => File.Delete($"{_savingFolder}/{key}");

		/// <inheritdoc />
		public bool Exists(string key) => File.Exists($"{_savingFolder}/{key}");
	}
}