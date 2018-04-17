using System.IO;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Data.Synchronization;

namespace WellFired.Guacamole.Integration.DataStorage
{
	[TestFixture]
	public class GivenAFileSystemDataWatcher
	{
		private const int TimeOut = 10000;
		
		[Test]
		public void When_Watch_A_Data_At_Specific_Key_And_Data_Is_Modified_Then_Get_Notified()
		{
			var dataLocation = Utils.GetTestDllRepository();
			var watchedData = dataLocation + "/options";

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
		public void Based_On_Focus_Callback_When_Watch_A_Data_At_Specific_Key_And_Data_Is_Modified_Then_Get_Notified()
		{
			var dataLocation = Utils.GetTestDllRepository();
			var watchedData = dataLocation + "/options";

			try
			{
				File.WriteAllText(watchedData, "original content");
				
				var application = new Application();
				var fileSystemDataWatcher = new FileSystemDataWatcher(dataLocation, () => application.HasFocus);
				
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
					Assert.Fail("The integration test did not timed out, it should have since application not having focus," +
					            " the change should not be notified yet.");
				}
				else
				{
					//We now simulate the application has focus, and we check if the data get notified in a reasonable time.
					manualReset.Reset();
					application.HasFocus = true;
					
					if (manualReset.WaitOne(TimeOut))
					{
						Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
					}
					else
					{
						Assert.Fail("The integration test timed out");
					}
				}
			}
			finally
			{
				File.Delete(watchedData);
			}
		}

		private class Application
		{
			public bool HasFocus { set; get; }
		}
	}
}