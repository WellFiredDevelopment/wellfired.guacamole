using System.IO;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.StoredData;

namespace WellFired.Guacamole.Unit.StoredData
{
	[TestFixture]
	public class Given_A_FileSystemDataWatcher
	{
		[Test]
		public void When_Watch_A_Data_At_Specific_Key_And_Data_Is_Modified_Then_Get_Notified()
		{
			var dataLocation = Utils.GetTestDllRepository();
			var watchedData = dataLocation + "/options.gdata";
			
			try
			{
				File.WriteAllText(watchedData, "original content");

				var fileSystemDataWatcher = new FileSystemDataWatcher(dataLocation);
				var listener = Substitute.For<IStoredDataWatcherListener>();
				fileSystemDataWatcher.SetListener(listener);

				fileSystemDataWatcher.Watch("options");

				Assert.That(() => listener.DidNotReceive().DoStoredDataChanged(Arg.Any<string>()), Throws.Nothing);

				File.WriteAllText(watchedData, "new content");
				Thread.Sleep(300);
				
				Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
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
				var listener = Substitute.For<IStoredDataWatcherListener>();
				fileSystemDataWatcher.SetListener(listener);

				fileSystemDataWatcher.Watch("options");

				Assert.That(() => listener.DidNotReceive().DoStoredDataChanged(Arg.Any<string>()), Throws.Nothing);

				File.WriteAllText(watchedData, "new content");
				Thread.Sleep(300);
				
				Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
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
				var listener = Substitute.For<IStoredDataWatcherListener>();
				fileSystemDataWatcher.SetListener(listener);

				fileSystemDataWatcher.Watch("options");

				File.Delete(watchedData);
				Thread.Sleep(300);				
				Assert.That(() => listener.Received(1).DoStoredDataChanged("options"), Throws.Nothing);
			}
			finally
			{
				File.Delete(watchedData);
			}
		}
	}
}