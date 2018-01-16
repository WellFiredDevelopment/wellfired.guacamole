using UnityEditor;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Unity.Editor.Platform
{
	/// <inheritdoc />
	/// <summary>
	/// An implementation of the Unity Persistent Data Storage Service. This service uses Unity Player prefs
	/// </summary>
	public class UnityPersonalDataStorageService : IDataStorageService
	{
		private readonly string _applicationName;
		
		public UnityPersonalDataStorageService(string applicationName)
		{
			_applicationName = applicationName;
		}

		/// <inheritdoc />
		/// <summary>
		/// Reads data from the Unity Personal Storage. This is a key value store, and reads directly from player prefs.
		/// To allow different applications to use the same key, the final key used is a combination of the key and the 
		/// application name.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string Read(string key)
		{
			return EditorPrefs.GetString($"{_applicationName}:{key}");
		}

		/// <inheritdoc />
		/// <summary>
		/// Write data into the Unity Personal Storage. This is a key value store, and writes directly into player prefs.
		/// To allow different applications to use the same key, the final key used is a combination of the key and the 
		/// application name.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			EditorPrefs.SetString($"{_applicationName}:{key}", data);
		}

		public void Delete(string key)
		{
			EditorPrefs.DeleteKey($"{_applicationName}:{key}");
		}
	}
}