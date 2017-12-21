using System.Collections.Generic;
using WellFired.Guacamole.StoredData;
using WellFired.Guacamole.Unity.Editor.Platform;

namespace WellFired.Guacamole.Unity.Editor.StoredData
{
	public class TeamSharedDataAccess
	{
		private static DataAccess _dataAccess;

		public static void SetupDataAccess(string applicationName, IEnumerable<IVersionUpdater> versionUpdaters = null)
		{
			_dataAccess = new DataAccess(
				new UnityTeamSharedDataStorageService(applicationName),
				new DataCacher(),
				new StoredDataUpdater(versionUpdaters),
				new FileSystemDataWatcher($"{new UnityPlatformProvider(applicationName).PlatformDataPath}"));
		}

		public static void RegisterDataProxy(string key, IDataProxy dataProxy)
		{
			_dataAccess.Track(key, dataProxy);
		}

		public static void SaveOnDisk(string key)
		{
			_dataAccess.Save(key);
		}
	}
}