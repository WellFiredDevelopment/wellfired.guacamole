using System.IO;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Unity.Editor.Platform
{
	public class UnityTeamSharedDataStorageService : IDataStorageService
	{
		private readonly string _applicationName;

		public UnityTeamSharedDataStorageService(string applicationName)
		{
			_applicationName = applicationName;
		}

		/// <inheritdoc />
		/// <summary>
		/// Reads data from the Team-Shared Persistent Storage. This is a key value store, and reads directly from a file inside
		/// the Unity project. The key is the name of the storage file. To allow different applications to use the same key, the 
		/// storage file is saved in a folder named with the application name.
		/// </summary>
		/// <param name="key"></param>
		/// <returns>The content stored. Returns null if nothing was saved at the key</returns>
		public string Read(string key)
		{
			string content = null;
			
			var assetPath = Device.AdjustPath($"{_applicationName}/{key}.gdata");
			if (File.Exists(assetPath))
			{
				content = File.ReadAllText(assetPath);
			}

			return content;
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes data to the Team-Shared Persistent Storage. This is a key value store, and write directly from a file inside
		/// the Unity project. The key is the name of the storage file. To allow different applications to use the same key, the 
		/// storage file is saved in a folder named with the application name.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			var assetPath = Device.AdjustPath($"{_applicationName}/{key}.gdata");
			if (!File.Exists(assetPath))
			{
				Directory.CreateDirectory(Device.AdjustPath($"{_applicationName}"));
			}
			
			File.WriteAllText(assetPath, data);
		}
	}
}