namespace WellFired.Guacamole.DataStorage.Synchronization
{
	public class ThreadSynchronizer
	{
		public static void InitializeSharedInstance(IKeyBasedReadWriteLock readWriteLock, bool forceReinitialization = false)
		{
			if (_sharedInstance != null && !forceReinitialization)
			{
				throw new AlreadyInitializeException();
			}
			
			_sharedInstance = new ThreadSynchronizer(readWriteLock);
		}

		public static ThreadSynchronizer SharedInstance => _sharedInstance ?? (_sharedInstance = new ThreadSynchronizer(new KeyBasedReadWriteLock()));

		private readonly IKeyBasedReadWriteLock _readWriteLock;
		private static ThreadSynchronizer _sharedInstance;

		private ThreadSynchronizer(IKeyBasedReadWriteLock readWriteLock)
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