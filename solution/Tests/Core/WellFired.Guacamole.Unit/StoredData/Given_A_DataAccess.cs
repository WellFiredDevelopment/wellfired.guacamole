using System;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Data;
using WellFired.Guacamole.DataStorage.Data.Synchronization;
using WellFired.Guacamole.DataStorage.Data.VersionUpdater;
using WellFired.Guacamole.DataStorage.Storages;

namespace WellFired.Guacamole.Unit.StoredData
{
	[TestFixture]
	public class GivenADataAccess
	{
		[Test]
		public void When_DataAccess_Is_Instanciated_Then_Stored_Data_Is_Updated_And_StoredDataWatcher_Listened()
		{
			var storedDataUpdater = Substitute.For<IStoredDataUpdater>();
			var storedDataWatcher = Substitute.For<IStoredDataWatcher>();
			var dataAccess = GetDataAccess(storedDataUpdated: storedDataUpdater, storedDataWatcher: storedDataWatcher);

			Assert.That(() => storedDataUpdater.Received(1).UpdateStoredData(), Throws.Nothing);
			Assert.That(() => storedDataWatcher.Received(1).SetListener(dataAccess), Throws.Nothing);
		}

		[Test]
		public void When_Track_Data_Located_At_Specific_Key_ThenFirstly_Data_Proxy_Cached_Then_Data_Loaded_In_Proxy_Then_StoredData_Changed_Are_Tracked()
		{
			var dataStorageService = Substitute.For<IDataStorageService>();
			dataStorageService.Read("Options").Returns("Serialized data");
			var dataCacher = Substitute.For<IDataCacher>();
			var storedDataWatcher = Substitute.For<IStoredDataWatcher>();

			var dataAccess = GetDataAccess(dataStorageService, dataCacher, storedDataWatcher: storedDataWatcher);
			var proxy = Substitute.For<IDataProxy>();

			dataAccess.Track("Options", proxy);

			Received.InOrder(
				() =>
				{
					dataCacher.Cache("Options", proxy);
					dataCacher.UpdateData("Options", "Serialized data");
					storedDataWatcher.Watch("Options");
				});
		}

		[Test]
		public void When_Track_Data_And_Data_Does_Not_Exist_Then_Cache_Initialize_With_Null_Value_And_Unexisting_Key_Is_Not_Read()
		{
			var dataStorageService = Substitute.For<IDataStorageService>();
			dataStorageService.Read("Options").Throws(new Exception("Data does not exists"));
			var dataCacher = Substitute.For<IDataCacher>();
			var storedDataWatcher = Substitute.For<IStoredDataWatcher>();

			var dataAccess = GetDataAccess(dataStorageService, dataCacher, storedDataWatcher: storedDataWatcher);
			var proxy = Substitute.For<IDataProxy>();

			dataAccess.Track("Options", proxy);
			
			Assert.That(() => dataCacher.Received(1).UpdateData("Options", null), Throws.Nothing);
		}

		[Test]
		public void
			When_SaveData_For_Specific_Key_And_CachedData_WasChanged_ThenFirstly_Suspend_DataWatcher_Then_Write_CachedData_To_Storage_Then_Resume_DataWatcher_Finally_Ensure_CachedData_Changed_State_Reset()
		{
			var dataStorageService = Substitute.For<IDataStorageService>();

			var dataCacher = Substitute.For<IDataCacher>();
			dataCacher.DidDataChanged("Options").Returns(true);
			dataCacher.GetData("Options").Returns("Serialized data");

			var storedDataWatcher = Substitute.For<IStoredDataWatcher>();

			var dataAccess = GetDataAccess(dataCacher: dataCacher, storedDataWatcher: storedDataWatcher, dataStorageService: dataStorageService);
			dataAccess.Save("Options");

			Received.InOrder(
				() =>
				{
					storedDataWatcher.Suspend("Options");
					dataStorageService.Write("Serialized data", "Options");
					storedDataWatcher.Resume("Options");
					dataCacher.ResetDataChanged("Options");
				});
		}

		[Test]
		public void When_Data_Changed_In_Storage_Then_Stored_Data_Is_Updated_And_Injected_In_Cached_Data_And_Data_Is_Saved()
		{
			var dataStorageService = Substitute.For<IDataStorageService>();
			dataStorageService.Read("Options").Returns("Serialized data");
			var dataCacher = Substitute.For<IDataCacher>();
			var storedDataUpdater = Substitute.For<IStoredDataUpdater>();

			var dataAccess = GetDataAccess(dataStorageService, dataCacher, storedDataUpdater);
			storedDataUpdater.ClearReceivedCalls();
			
			dataAccess.DoStoredDataChanged("Options");

			Received.InOrder(
				() =>
				{
					storedDataUpdater.UpdateStoredData();
					dataCacher.UpdateData("Options", "Serialized data");
					dataAccess.Received(1).Save("Options");
				});
		}

		private static DataAccess GetDataAccess(
			IDataStorageService dataStorageService = null,
			IDataCacher dataCacher = null,
			IStoredDataUpdater storedDataUpdated = null,
			IStoredDataWatcher storedDataWatcher = null
		)
		{
			return Substitute.For<DataAccess>(
				dataStorageService ?? Substitute.For<IDataStorageService>(),
				dataCacher ?? Substitute.For<IDataCacher>(),
				storedDataUpdated ?? Substitute.For<IStoredDataUpdater>(),
				storedDataWatcher ?? Substitute.For<IStoredDataWatcher>()
			);
		}
	}
}