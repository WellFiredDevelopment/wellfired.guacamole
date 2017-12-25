using System.IO;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.StoredData;

namespace WellFired.Guacamole.Integration.StoredData
{
	[TestFixture]
	public class Given_A_FileSystemDataWatcher
	{
		private const int TimeOut = 10000;
		
		[Test]
		public void When_Watch_A_Data_At_Specific_Key_And_Data_Is_Modified_Then_Get_Notified()
		{
			var dataLocation = Utils.GetTestDllRepository();
			var watchedData = dataLocation + "/options.gdata";

			try
			{
				File.WriteAllText(watchedData, "original content");

				var fileSystemDataWatcher = new FileSystemDataWatcher(dataLocation);
				
				var manualReset = new ManualResetEvent(false);
				var listener = Substitute.For<IStoredDataWatcherListener>();
				listener.When(x => x.DoStoredDataChanged(Arg.Any<string>())).Do(x =>
				{
					manualReset.Set();
				});
				fileSystemDataWatcher.SetListener(listener);
				
				fileSystemDataWatcher.Watch("options");
				
				Assert.That(() => listener.DidNotReceive().DoStoredDataChanged(Arg.Any<string>()), Throws.Nothing);

				Thread.Sleep(1000); //Need to wait here to pass the test which I cannot explain why. We are more testing interaction rather
				//than FileSystemWatcher or File.WriteAllText implementation. But would be great to be able to find the reason. 
				
				File.WriteAllText(watchedData, "new content");

				if (manualReset.WaitOne(TimeOut))
				{
					Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
				}
				else
				{
					Assert.Fail("The integration test timed out");
				}
			}
			finally
			{
				File.Delete(watchedData);
			}
		}
		
		[Test]
		public void When_Watch_A_Data_At_Specific_Key_And_Data_Is_Created_Then_Get_Notified()
		{
			var dataLocation = Utils.GetTestDllRepository();
			var watchedData = dataLocation + "/options.gdata";
			
			try
			{
				var fileSystemDataWatcher = new FileSystemDataWatcher(dataLocation);
				var manualReset = new ManualResetEvent(false);
				var listener = Substitute.For<IStoredDataWatcherListener>();
				listener.When(x => x.DoStoredDataChanged(Arg.Any<string>())).Do(x =>
				{
					manualReset.Set();
				});
				
				fileSystemDataWatcher.SetListener(listener);

				fileSystemDataWatcher.Watch("options");

				Assert.That(() => listener.DidNotReceive().DoStoredDataChanged(Arg.Any<string>()), Throws.Nothing);

				File.WriteAllText(watchedData, "new content");
				
				if (manualReset.WaitOne(TimeOut))
				{
					Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
				}
				else
				{
					Assert.Fail("The integration test timed out");
				}
			}
			finally
			{
				File.Delete(watchedData);
			}
		}
		
		[Test]
		public void When_Watch_A_Data_At_Specific_Key_And_Data_Is_Deleted_Then_Get_Notified()
		{
			var dataLocation = Utils.GetTestDllRepository();
			var watchedData = dataLocation + "/options.gdata";
			
			try
			{
				File.WriteAllText(watchedData, "new content");
				
				var fileSystemDataWatcher = new FileSystemDataWatcher(dataLocation);
				
				var manualReset = new ManualResetEvent(false);
				var listener = Substitute.For<IStoredDataWatcherListener>();
				listener.When(x => x.DoStoredDataChanged(Arg.Any<string>())).Do(x =>
				{
					manualReset.Set();
				});
				
				fileSystemDataWatcher.SetListener(listener);
				fileSystemDataWatcher.Watch("options");

				File.Delete(watchedData);
				
				if (manualReset.WaitOne(TimeOut))
				{
					Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
				}
				else
				{
					Assert.Fail("The integration test timed out");
				}
			}
			finally
			{
				File.Delete(watchedData);
			}
		}
	}
}