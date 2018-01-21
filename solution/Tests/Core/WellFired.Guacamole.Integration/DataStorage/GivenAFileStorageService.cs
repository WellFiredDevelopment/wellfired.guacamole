using System.IO;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Storages;

namespace WellFired.Guacamole.Integration.DataStorage
{
	[TestFixture]
	public class GivenAFileStorageService
	{
		[Test]
		public void When_Instantiate_With_Unexisting_Directory_Then_Create_Directory_And_Successfully_Save_Data_Inside()
		{
			var dllPath = Utils.GetTestDllRepository();

			try
			{
				var fileStorageService = new FileStorageService(dllPath + "/UnexistingFolder");
				fileStorageService.Write("some data", "Options");
				Assert.That(fileStorageService.Read("Options"), Is.EqualTo("some data"));
			}
			finally
			{
				Directory.Delete(dllPath + "/UnexistingFolder", true);
			}
		}

		[Test]
		public void When_File_Does_Not_Exist_Then_Return_False()
		{
			var dllPath = Utils.GetTestDllRepository();

			var fileStorageService = new FileStorageService(dllPath);
			Assert.IsFalse(fileStorageService.Exists("Options"));
		}
		
		[Test]
		public void When_File_Exist_Then_Return_True()
		{
			var dllPath = Utils.GetTestDllRepository();
			var fileStorageService = new FileStorageService(dllPath);

			try
			{
				fileStorageService.Write("some data", "Options");
				Assert.IsTrue(fileStorageService.Exists("Options"));
			}
			finally
			{
				File.Delete(dllPath + "/Options");
			}
		}
	}
}