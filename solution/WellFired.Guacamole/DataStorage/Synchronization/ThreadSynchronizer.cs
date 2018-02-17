namespace WellFired.Guacamole.DataStorage.Synchronization
{
	public class ThreadSynchronizer
	{
		private readonly IKeyBasedReadWriteLock _readWriteLock;

		public ThreadSynchronizer(IKeyBasedReadWriteLock readWriteLock)
		{
			_readWriteLock = readWriteLock;
		}
		
		public void EnterReadLock(string key)
		{
			_readWriteLock.EnterReadLock(key);
		}

		public void ExitReadLock(string key)
		{
			_readWriteLock.ExitReadLock(key);
		}

		public void EnterWriteLock(string key)
		{
			_readWriteLock.EnterWriteLock(key);
		}

		public void ExitWriteLock(string key)
		{
			_readWriteLock.ExitWriteLock(key);
		}
	}
}