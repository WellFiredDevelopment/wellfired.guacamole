namespace WellFired.Guacamole.StoredData
{
	public interface IStoredDataWatcher
	{
		void Watch(string key);
		void Suspend(string key);
		void Resume(string key);
		void SetListener(IStoredDataWatcherListener listener);
	}

	public interface IStoredDataWatcherListener
	{
		void DoStoredDataChanged(string key);
	}
}