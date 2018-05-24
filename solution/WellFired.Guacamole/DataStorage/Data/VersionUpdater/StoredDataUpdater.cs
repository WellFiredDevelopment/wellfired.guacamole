using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.DataStorage.Data.VersionUpdater
{
	public class StoredDataUpdater : IStoredDataUpdater
	{
		private readonly IVersionUpdater[] _versionUpdaters;

		public StoredDataUpdater(IEnumerable<IVersionUpdater> versionUpdaters = null)
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

			foreach (var versionUpdater in _versionUpdaters)
			{
				if (startUpdate)
				{
					Logger.LogMessage($"Update data to version {versionUpdater.VersionNo}");
					versionUpdater.UpdatePreviousVersion();
				}
				else if (versionUpdater.IsCompatibleWithCurrentVersion())
				{
					startUpdate = true;
				}
			}
		}
	}
}