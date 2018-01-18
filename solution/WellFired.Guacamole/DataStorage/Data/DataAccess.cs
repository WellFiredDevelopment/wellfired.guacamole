using System;
using WellFired.Guacamole.DataStorage.Data.Synchronization;
using WellFired.Guacamole.DataStorage.Data.VersionUpdater;
using WellFired.Guacamole.DataStorage.Storages;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.DataStorage.Data
{
	/// <summary>
	/// DataAccess is a hub to access data provided by a <see cref="IDataStorageService"/>". It offers :
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
	/// <seealso cref="IDataProxy"/><seealso cref="DataProxy{T}"/>
	/// </summary>
	public class DataAccess : IDataAccess, IStoredDataWatcherListener
	{
		private readonly IDataStorageService _dataStorageService;
		private readonly IDataCacher _dataCacher;
		private readonly IStoredDataUpdater _storedDataUpdater;
		private readonly IStoredDataWatcher _storedDataWatcher;

		public DataAccess(IDataStorageService dataStorageService, IDataCacher dataCacher, IStoredDataUpdater storedDataUpdater,
			IStoredDataWatcher storedDataWatcher = null)
		{
			_dataStorageService = dataStorageService;
			_dataCacher = dataCacher;
			_storedDataUpdater = storedDataUpdater;
			_storedDataWatcher = storedDataWatcher;
			
			Initialize();
		}

		/// <summary>
		/// Load stored data in your data proxy and ensure any changes to the stored data is propagated to your data proxy.
		/// </summary>
		/// <param name="key">The key where is located the data</param>
		/// <param name="dataProxy">Your data proxy. An implementation of the proxy is provided by <see cref="DataProxy{T}"/></param>
		public void Track(string key, IDataProxy dataProxy)
		{
			_dataCacher.Cache(key, dataProxy);
			_dataCacher.UpdateData(key, _dataStorageService.Read(key));
			_storedDataWatcher?.Watch(key);
		}

		/// <summary>
		/// Force <see cref="DataAccess"/> to save the data from your data proxy in the storage. It will save it only if data changed in your proxy.
		/// </summary>
		/// <param name="key">The key where is located the data</param>
		public void Save(string key)
		{
			if (!_dataCacher.DidDataChanged(key))
			{
				return;
			}
			
			_storedDataWatcher?.Suspend(key);
			_dataStorageService.Write(_dataCacher.GetData(key), key);
			_storedDataWatcher?.Resume(key);
			
			_dataCacher.ResetDataChanged(key);
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
			try
			{
				_storedDataUpdater.UpdateStoredData();
			}
			catch (Exception e)
			{
				Logger.LogError("An error happened when updating the stored data. The data will " +
				               "be reinitilized to default values. Error details :\n" + e.Message + "\n" + e.StackTrace);
			}
		}
	}
}