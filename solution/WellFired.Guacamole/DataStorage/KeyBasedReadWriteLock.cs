using System;
using System.Collections.Generic;
using System.Threading;

namespace WellFired.Guacamole.DataStorage
{
	public class KeyBasedReadWriteLock : System.IDisposable
	{
		private readonly Dictionary<string, ReaderWriterLockSlim> _storageLocks = new Dictionary<string, ReaderWriterLockSlim>();

		private enum Mode
		{
			Write,
			Read
		}

		public void EnterReadLock(string path)
		{
			EnterLock(path, Mode.Read);
		}

		public void EnterWriteLock(string path)
		{
			EnterLock(path, Mode.Write);
		}

		public void ExitReadLock(string path)
		{
			ExitLock(path, Mode.Read);
		}

		public void ExitWriteLock(string path)
		{
			ExitLock(path, Mode.Write);
		}

		private void EnterLock(string path, Mode mode)
		{
			lock (_storageLocks)
			{
				if (!_storageLocks.ContainsKey(path))
					_storageLocks.Add(path, new ReaderWriterLockSlim());

				Action enterAction;

				switch (mode)
				{
					case Mode.Write:
						enterAction = _storageLocks[path].EnterWriteLock;
						break;
					case Mode.Read:
						enterAction = _storageLocks[path].EnterReadLock;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
				}

				enterAction();
			}
		}

		private void ExitLock(string path, Mode mode)
		{
			lock (_storageLocks)
			{
				Action exitAction;

				switch (mode)
				{
					case Mode.Write:
						exitAction = _storageLocks[path].ExitWriteLock;
						break;
					case Mode.Read:
						exitAction = _storageLocks[path].ExitReadLock;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
				}

				if (_storageLocks[path].WaitingReadCount == 0
				    && _storageLocks[path].WaitingWriteCount == 0
				    && _storageLocks[path].WaitingUpgradeCount == 0
				    && _storageLocks[path].CurrentReadCount == 1)
				{
					exitAction();
					_storageLocks[path].Dispose();
					_storageLocks.Remove(path);
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