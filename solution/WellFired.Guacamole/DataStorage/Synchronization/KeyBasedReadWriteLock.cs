using System;
using System.Collections.Generic;
using System.Threading;

namespace WellFired.Guacamole.DataStorage.Synchronization
{
	public class KeyBasedReadWriteLock : IKeyBasedReadWriteLock, System.IDisposable
	{
		private enum Mode
		{
			Write,
			Read
		}
		
		private readonly Dictionary<string, ReaderWriterLockSlim> _storageLocks = new Dictionary<string, ReaderWriterLockSlim>();
		
		public void EnterReadLock(string key)
		{
			EnterLock(key, Mode.Read);
		}

		public void ExitReadLock(string key)
		{
			ExitLock(key, Mode.Read);
		}

		public void EnterWriteLock(string key)
		{
			EnterLock(key, Mode.Write);
		}

		public void ExitWriteLock(string key)
		{
			ExitLock(key, Mode.Write);
		}
		
		private void EnterLock(string key, Mode mode)
		{
			lock (_storageLocks)
			{
				if (!_storageLocks.ContainsKey(key))
					_storageLocks.Add(key, new ReaderWriterLockSlim());

				Action enterAction;

				switch (mode)
				{
					case Mode.Write:
						enterAction = _storageLocks[key].EnterWriteLock;
						break;
					case Mode.Read:
						enterAction = _storageLocks[key].EnterReadLock;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
				}

				enterAction();
			}
		}
		
		private void ExitLock(string key, Mode mode)
		{
			lock (_storageLocks)
			{
				Action exitAction;

				switch (mode)
				{
					case Mode.Write:
						exitAction = _storageLocks[key].ExitWriteLock;
						break;
					case Mode.Read:
						exitAction = _storageLocks[key].ExitReadLock;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
				}

				if (_storageLocks[key].WaitingReadCount == 0
				    && _storageLocks[key].WaitingWriteCount == 0
				    && _storageLocks[key].WaitingUpgradeCount == 0
				    && _storageLocks[key].CurrentReadCount <= 1)
				{
					exitAction();
					_storageLocks[key].Dispose();
					_storageLocks.Remove(key);
				}
				else
				{
					exitAction();
				}
			}
		}
		
		~KeyBasedReadWriteLock()
		{
			Dispose();
		}

		public void Dispose()
		{
			lock (_storageLocks)
			{
				foreach (KeyValuePair<string, ReaderWriterLockSlim> writerLockSlim in _storageLocks)
				{
					writerLockSlim.Value.Dispose();
				}
			}
		}
	}
}