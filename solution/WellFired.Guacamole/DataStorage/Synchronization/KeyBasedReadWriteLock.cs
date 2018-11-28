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

		private readonly Dictionary<string, ReaderWriterLockSlim> _readWriteLocks = new Dictionary<string, ReaderWriterLockSlim>();
		private readonly Dictionary<string, int> _waitingCounter = new Dictionary<string, int>();

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
			lock (_waitingCounter)
			{
				if (!_waitingCounter.ContainsKey(key))
					_waitingCounter.Add(key, 0);

				_waitingCounter[key]++;
			}

			Action enterAction;

			lock (_readWriteLocks)
			{
				if (!_readWriteLocks.ContainsKey(key))
					_readWriteLocks.Add(key, new ReaderWriterLockSlim());

				switch (mode)
				{
					case Mode.Write:
						enterAction = _readWriteLocks[key].EnterWriteLock;
						break;
					case Mode.Read:
						enterAction = _readWriteLocks[key].EnterReadLock;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
				}
			}

			enterAction();
		}

		private void ExitLock(string key, Mode mode)
		{
			lock (_readWriteLocks)
			{
				Action exitAction;
				
				switch (mode)
				{
					case Mode.Write:
						exitAction = _readWriteLocks[key].ExitWriteLock;
						break;
					case Mode.Read:
						exitAction = _readWriteLocks[key].ExitReadLock;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
				}

				lock (_waitingCounter)
				{
					_waitingCounter[key]--;
					if (_waitingCounter[key] == 0)
					{
						_waitingCounter.Remove(key);
						_readWriteLocks.Remove(key);
					}
				}

				exitAction();
			}
		}

		~KeyBasedReadWriteLock()
		{
			Dispose();
		}

		public void Dispose()
		{
			lock (_readWriteLocks)
			{
				foreach (KeyValuePair<string, ReaderWriterLockSlim> writerLockSlim in _readWriteLocks)
				{
					writerLockSlim.Value.Dispose();
				}
			}
		}
	}
}