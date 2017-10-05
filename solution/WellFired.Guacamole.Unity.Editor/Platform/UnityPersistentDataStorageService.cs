using UnityEngine;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Unity.Editor.Platform
{
	/// <inheritdoc />
	/// <summary>
	/// An implementation of the Unity Persistent Data Storage Service. This service uses Unity Player prefs
	/// </summary>
	public class UnityPersistentDataStorageService : IPersistentDataStorageService
	{
		/// <inheritdoc />
		/// <summary>
		/// Reads data from the Unity Persistent Storage. This is a key value store, and reads directly from player prefs.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string Read(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		/// <inheritdoc />
		/// <summary>
		/// Write data into the Unity Persistent Storage. This is a key value store, and writes directly into player prefs.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		public void Write(string data, string key)
		{
			PlayerPrefs.SetString(key, data);
		}
	}
}