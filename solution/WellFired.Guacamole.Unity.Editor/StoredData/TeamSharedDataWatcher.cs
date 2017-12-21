using System;
using System.Collections.Generic;
using System.IO;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.StoredData;
using WellFired.Guacamole.Unity.Editor.Platform;

namespace WellFired.Guacamole.Unity.Editor.StoredData
{
	public class TeamSharedDataWatcher : IStoredDataWatcher
	{
		private readonly string _dataPath;
		private readonly Dictionary<string, FileSystemWatcher> _fileSystemWatchers = new Dictionary<string, FileSystemWatcher>();
		private IStoredDataWatcherListener _listener;

		public TeamSharedDataWatcher(string applicationName)
		{
			_dataPath = $"{new UnityPlatformProvider(applicationName).PlatformDataPath}";
			//FileSystemWatcher needs that to work in Mono CRT
			Environment.SetEnvironmentVariable("MONO_MANAGED_WATCHER", "enabled");
		}

		public void Watch(string key)
		{
			_fileSystemWatchers.Add(key, null);
			Resume(key);
		}

		public void Suspend(string key)
		{
			_fileSystemWatchers[key].Changed -= File_OnChanged;
			_fileSystemWatchers[key].Dispose();
			_fileSystemWatchers[key] = null;
		}

		public void Resume(string key)
		{
			var fileSystemWatcher = new FileSystemWatcher {
				Path = $"{_dataPath}/{key}.gdata",
				EnableRaisingEvents = true
			};
			fileSystemWatcher.Changed += File_OnChanged;

			_fileSystemWatchers[key] = fileSystemWatcher;
		}

		public void SetListener(IStoredDataWatcherListener listener)
		{
			_listener = listener;
		}

		private void File_OnChanged(object sender, FileSystemEventArgs e)
		{
			switch (e.ChangeType)
			{
				case WatcherChangeTypes.Changed:
					MainThreadRunner.ExecuteOnMainThread(() => { _listener?.DoStoredDataChanged(Path.GetFileNameWithoutExtension(e.Name)); });
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}