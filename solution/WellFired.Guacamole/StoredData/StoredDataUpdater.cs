using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.StoredData
{
	public class StoredDataUpdater : IStoredDataUpdater
	{
		public int CurrentVersion => GetLatestVersionUpdater().VersionNo;
		
		private readonly IVersionUpdater[] _versionUpdaters;

		public StoredDataUpdater(IEnumerable<IVersionUpdater> versionUpdaters)
		{
			_versionUpdaters = 
				versionUpdaters?.OrderBy(versionUpdater => versionUpdater.VersionNo).ToArray() 
				?? new IVersionUpdater[0];
		}

		public void UpdateStoredData()
		{
			Update();
		}

		private void Update()
		{
			var startUpdate = false;

			foreach (IVersionUpdater versionUpdater in _versionUpdaters)
			{
				if (startUpdate)
				{
					Logger.LogMessage($"Update settings data file to version {versionUpdater.VersionNo}");
					versionUpdater.UpdatePreviousVersion();
				}
				else if (versionUpdater.IsCompatibleWithCurrentVersion())
				{
					startUpdate = true;
				}
			}
		}

		private IVersionUpdater GetLatestVersionUpdater()
		{
			return _versionUpdaters[_versionUpdaters.Length - 1];
		}
	}
}