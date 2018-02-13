namespace WellFired.Guacamole.DataStorage.Synchronization
{
	public interface IKeyBasedReadWriteLock
	{
		void EnterReadLock(string key);
		void ExitReadLock(string key);
		void EnterWriteLock(string key);
		void ExitWriteLock(string key);
	}
}