using System;
using System.Collections.Generic;
using System.IO;

namespace WellFired.Guacamole.DataStorage.Data.Synchronization
{
	public class FileSystemDataWatcher : IStoredDataWatcher
	{
		private readonly string _dataPath;
		private readonly Dictionary<string, FileSystemWatcher> _fileSystemWatchers = new Dictionary<string, FileSystemWatcher>();
		private IStoredDataWatcherListener _listener;

		public FileSystemDataWatcher(string dataPath)
		{
			_dataPath = dataPath;

			//FileSystemWatcher needs that to work in Mono CRT
			Environment.SetEnvironmentVariable("MONO_MANAGED_WATCHER", "enabled");
		}

		public void Watch(string key)
		{
			lock (_fileSystemWatchers)
			{
				_fileSystemWatchers.Add(key, null);
			}
			
			Resume(key);
		}

		public void Suspend(string key)
		{
			lock (_fileSystemWatchers)
			{
				_fileSystemWatchers[key].Changed -= File_OnChanged;
				_fileSystemWatchers[key].Dispose();
				_fileSystemWatchers[key] = null;
			}
		}

		public void Resume(string key)
		{
			var fileSystemWatcher = new FileSystemWatcher {
				Path = _dataPath,
				Filter = key,
				EnableRaisingEvents = true
			};
			
			fileSystemWatcher.Changed += File_OnChanged;

			lock (fileSystemWatcher)
			{
				_fileSystemWatchers[key] = fileSystemWatcher;
			}
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
					_listener?.DoStoredDataChanged(Path.GetFileNameWithoutExtension(e.Name));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}