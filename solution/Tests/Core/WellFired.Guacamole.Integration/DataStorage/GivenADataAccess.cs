using System.IO;
using System.Threading;
using JetBrains.Annotations;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Data;
using WellFired.Guacamole.DataStorage.Data.Synchronization;
using WellFired.Guacamole.DataStorage.Data.VersionUpdater;
using WellFired.Guacamole.DataStorage.Storages;

namespace WellFired.Guacamole.Integration.DataStorage
{
	[TestFixture]
	public class GivenADataAccess
	{
		private const int TimeOut = 5000;
		private const int ThreadNumber = 100;
		
		[Test]
		public void Test_Potential_DeadLock()
		{
			var dataLocation = Utils.GetTestDllRepository();
			
			try
			{
				const string synchroId = "synchroID";

				var dataAccess1 = new DataAccess(
					new FileStorageService(dataLocation),
					new DataCacher(),
					new StoredDataUpdater(),
					new FileSystemDataWatcher(dataLocation),
					synchroId);

				var dataAccess2 = new DataAccess(
					new FileStorageService(dataLocation),
					new DataCacher(),
					new StoredDataUpdater(),
					new FileSystemDataWatcher(dataLocation),
					synchroId);

				var proxy1 = new OptionsProxy();
				var proxy2 = new OptionsProxy();

				dataAccess1.Track("Options", proxy1);
				dataAccess2.Track("Options", proxy2);

				var thread = GetSavingThread(dataAccess1, dataAccess2, proxy1, proxy2);
				thread.Start();
				if (!thread.Join(TimeOut))
				{
					Assert.Fail("Deadlock detected");
				}
				else
				{
					Assert.Pass();
				}
			}
			finally
			{
				File.Delete(dataLocation + "/Options");
			}
		}

		private static Thread GetSavingThread(DataAccess dataAccess1, DataAccess dataAccess2, OptionsProxy proxy1, OptionsProxy proxy2)
		{
			var thread = new Thread(() =>
			{
				var endedThreadsCount = 0;

				for (int i = 0; i < ThreadNumber; i++)
				{
					new Thread(() =>
					{
						SaveData(dataAccess1, proxy1);
						endedThreadsCount++;
					}).Start();

					new Thread(() =>
					{
						SaveData(dataAccess2, proxy2);
						endedThreadsCount++;
					}).Start();
				}

				while (endedThreadsCount < ThreadNumber * 2)
				{
				}
			});

			return thread;
		}

		private static void SaveData(IDataAccess dataAccess, OptionsProxy proxy)
		{
			proxy.Option1 = !proxy.Option1;
			dataAccess.Save("Options");
		}
		
		private class Options
		{
			[PublicAPI]
			public bool Option1;

			public Options()
			{
				Option1 = true;
			}
		}

		private class OptionsProxy : DataProxy<Options>
		{
			private bool _option1;
			
			public bool Option1
			{
				get => _option1;
				set => SetProperty(ref _option1, value);
			}
		} 
	}
}