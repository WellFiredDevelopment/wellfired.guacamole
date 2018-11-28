using System;
using System.IO;
using JetBrains.Annotations;
using WellFired.Guacamole.DataStorage.Data.Synchronization;
using WellFired.Guacamole.DataStorage.Data.VersionUpdater;
using WellFired.Guacamole.DataStorage.Exceptions;
using WellFired.Guacamole.DataStorage.Synchronization;
using WellFired.Guacamole.DataStorage.Types;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.DataStorage.Data
{
	/// <summary>
	/// <see cref="DataAccess"/> is a hub to access data provided by a <see cref="IDataStorageService"/>". It offers :
	/// <list type="bullet">
	/// <item>
	/// <description>the possibility to synchronize your <see cref="IDataProxy"/> with the stored data if ever it is changed (by modifying a file on a file storage for example)</description>
	/// </item>
	/// <item>
	/// <description>A mechanism to ensure your data is always updated to its last version before to track it</description>
	/// </item>
	/// <item>
	/// <description>The possibility to save content of your <see cref="IDataProxy"/> data in the storage only when you request it and only if data in your proxy was modified, 
	/// avoiding to constantly write to the storage</description>
	/// </item>
	/// </list>
	/// Note that a <see cref="DataAccess"/> is thread safe with himself. If ever you have several instance of it tracking data
	/// in the same emplacement, you may want to make use of the possibility of assigning a thread synchronizer id in the constructor method.
	/// <seealso cref="IDataProxy"/><seealso cref="DataProxy{T}"/>
	/// </summary>
	[PublicAPI]
	public class DataAccess : IDataAccess, IStoredDataWatcherListener
	{
		#region Thread Synchronization
		
		public static void InitializeSharedThreadLock(IKeyBasedReadWriteLock readWriteLock, bool forceReinitialization = false)
		{
			if (_sharedThreadLock != null && !forceReinitialization)
			{
				throw new AlreadyInitializeException();
			}
			
			_sharedThreadLock = new ThreadSynchronizer(readWriteLock);
		}

		private static ThreadSynchronizer SharedThreadLock => _sharedThreadLock ?? (_sharedThreadLock = new ThreadSynchronizer(new KeyBasedReadWriteLock()));
		private static ThreadSynchronizer _sharedThreadLock;
		
		#endregion
		
		private readonly IDataStorageService _dataStorageService;
		private readonly IDataCacher _dataCacher;
		private readonly IStoredDataUpdater _storedDataUpdater;
		private readonly IStoredDataWatcher _storedDataWatcher;
		private readonly string _synchronizeID;

		/// <summary>
		/// Constructor of <see cref="DataAccess"/>
		/// </summary>
		/// <param name="dataStorageService">The key base storage service where data is stored</param>
		/// <param name="dataCacher">The cacher ensuring the cached data is updated correctly</param>
		/// <param name="storedDataUpdater">The object in charge of updating the data to its current version</param>
		/// <param name="storedDataWatcher">An optional data watcher if the storage offers the possibility to detect data changes.</param>
		/// <param name="synchronizeID">Two <see cref="DataAccess"/> sharing the same id will work in a thread safe environment.
		/// For example, data will not be read while it is being updated. Note that it is key based. If one key of the storage is
		/// being saved, it will not prevent other threads from writing in different key locations. Also, if no id is specified
		/// then a unique id based on .Net GUID implementation will be generated.</param>
		public DataAccess(IDataStorageService dataStorageService, IDataCacher dataCacher, IStoredDataUpdater storedDataUpdater,
			IStoredDataWatcher storedDataWatcher = null, string synchronizeID = null)
		{
			_dataStorageService = dataStorageService;
			_dataCacher = dataCacher;
			_storedDataUpdater = storedDataUpdater;
			_storedDataWatcher = storedDataWatcher;
			
			_synchronizeID = synchronizeID ?? Guid.NewGuid().ToString();
			
			Initialize();
		}

		/// <summary>
		/// Load stored data in your data proxy and ensure any changes to the stored data is propagated to your data proxy.
		/// </summary>
		/// <param name="key">The key where is located the data</param>
		/// <param name="dataProxy">Your data proxy. An implementation of the proxy is provided by <see cref="DataProxy{T}"/></param>
		public void Track(string key, IDataProxy dataProxy)
		{
			var lockKey = Path.Combine(key, _synchronizeID);
			SharedThreadLock.EnterReadLock(lockKey);
			SharedThreadLock.EnterReadLock(_synchronizeID);

			try
			{
				_dataCacher.Cache(key, dataProxy);

				string storedData = null;
				if (_dataStorageService.Exists(key))
					storedData = _dataStorageService.Read(key);
			
				_dataCacher.UpdateData(key, storedData);
				_storedDataWatcher?.Watch(key);
			}
			finally 
			{
				SharedThreadLock.ExitReadLock(lockKey);
				SharedThreadLock.ExitReadLock(_synchronizeID);
			}
		}

		/// <summary>
		/// Force <see cref="DataAccess"/> to save the data from your data proxy in the storage. It will save it only if data changed in your proxy.
		/// </summary>
		/// <param name="key">The key where is located the data</param>
		public void Save(string key)
		{
			var lockKey = Path.Combine(key, _synchronizeID);
			SharedThreadLock.EnterWriteLock(lockKey);
			SharedThreadLock.EnterReadLock(_synchronizeID);
		
			try
			{
				if (_dataStorageService.Exists(key) && !_dataCacher.DidDataChanged(key))
				{
					return;
				}
				
				_storedDataWatcher?.Suspend(key);
				_dataStorageService.Write(_dataCacher.GetData(key), key);
				_storedDataWatcher?.Resume(key);
				
				_dataCacher.ResetDataChanged(key);
			}
			finally 
			{
				SharedThreadLock.ExitWriteLock(lockKey);
				SharedThreadLock.ExitReadLock(_synchronizeID);
			}
		}

		private void Initialize()
		{
			_storedDataWatcher?.SetListener(this);
			UpdateStoredData();
		}

		public void DoStoredDataChanged(string key)
		{
			UpdateStoredData();
			_dataCacher.UpdateData(key, _dataStorageService.Read(key));
			//we save the data in case the proxy modified it while loading it. This could be the case for a version file
			//where the version number needs to be enforced.
			Save(key);
		}

		private void UpdateStoredData()
		{
			SharedThreadLock.EnterWriteLock(_synchronizeID);
			
			try
			{
				_storedDataUpdater.UpdateStoredData();
			}
			catch (Exception e)
			{
				Logger.LogError("An error happened when updating the stored data. If reading the data fails, then" +
								" data default value may be used. Error details :\n" + e.Message + "\n" + e.StackTrace);
			}
			finally 
			{
				SharedThreadLock.ExitWriteLock(_synchronizeID);
			}
		}
	}
}