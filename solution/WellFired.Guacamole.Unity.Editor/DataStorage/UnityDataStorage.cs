using UnityEditor;
using WellFired.Guacamole.DataStorage.Storages;

namespace WellFired.Guacamole.Unity.Editor.DataStorage
{
	public class UnityDataStorage : IDataStorageService
	{
		public UnityDataStorage(string location)
		{
			Location = location;
		}

		public string Location { get; }

		private string GetLocation(string key) => $"{Location}/{key}";
		
		public string Read(string key)
		{
			return EditorPrefs.GetString(GetLocation(key));
		}

		public void Write(string data, string key)
		{
			EditorPrefs.SetString(GetLocation(key), data);
		}

		public void Delete(string key)
		{
			EditorPrefs.DeleteKey(GetLocation(key));
		}

		public bool Exists(string key)
		{
			return EditorPrefs.HasKey(GetLocation(key));
		}
	}
}