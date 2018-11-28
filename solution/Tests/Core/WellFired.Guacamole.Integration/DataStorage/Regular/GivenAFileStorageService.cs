using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Synchronization;
using WellFired.Guacamole.DataStorage.Types;

namespace WellFired.Guacamole.Integration.DataStorage.Regular
{
	[TestFixture]
	public class GivenAFileStorageService
	{
		[Test]
		public void When_Instantiate_With_Non_Existent_Directory_Then_Create_Directory_And_Successfully_Save_Data_Inside()
		{
			var dllPath = Utils.GetTestDllRepository();

			try
			{
				var fileStorageService = new FileStorageService(dllPath + "/NonExistentFolder");
				fileStorageService.Write("some data", "Options");
				Assert.That(fileStorageService.Read("Options"), Is.EqualTo("some data"));
			}
			finally
			{
				Directory.Delete(dllPath + "/NonExistentFolder", true);
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

		[Test]
		public void When_Read_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			FileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();

			var fileStorageService = new FileStorageService($"{dllPath}/storage");

			var exceptionNotThrown = false;
			try
			{
				fileStorageService.Read("aKey");
				exceptionNotThrown = true;
			}
			catch (Exception)
			{
				Assert.That(() => keyBasedLocker.Received(1).EnterReadLock($"{dllPath}/storage" + "aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitReadLock($"{dllPath}/storage" + "aKey"), Throws.Nothing);
			}
			finally
			{
				Directory.Delete($"{dllPath}/storage", true);
			}

			if (exceptionNotThrown)
			{
				Assert.Fail("An exception should be thrown when reading a non existing key.");
			}
		}

		[Test]
		public void When_Write_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			FileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();

			try
			{
				var fileStorageService = new FileStorageService($"{dllPath}/storage");
				fileStorageService.Write("Cow", "aKey");
				Assert.That(() => keyBasedLocker.Received(1).EnterWriteLock($"{dllPath}/storage" + "aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitWriteLock($"{dllPath}/storage" + "aKey"), Throws.Nothing);
			}
			finally
			{
				Directory.Delete($"{dllPath}/storage", true);
			}
		}

		[Test]
		public void When_Check_Exists_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			FileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();
			var fileStorageService = new FileStorageService($"{dllPath}");

			fileStorageService.Exists("aKey");

			Assert.That(() => keyBasedLocker.Received(1).EnterReadLock($"{dllPath}aKey"), Throws.Nothing);
			Assert.That(() => keyBasedLocker.Received(1).ExitReadLock($"{dllPath}aKey"), Throws.Nothing);
		}

		[Test]
		public void When_Delete_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			FileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();

			try
			{
				var fileStorageService = new FileStorageService($"{dllPath}/storage");
				fileStorageService.Delete("aKey");
				
				Assert.That(() => keyBasedLocker.Received(1).EnterWriteLock($"{dllPath}/storage" + "aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitWriteLock($"{dllPath}/storage" + "aKey"), Throws.Nothing);
			}
			finally
			{
				Directory.Delete($"{dllPath}/storage", true);
			}
		}
	}
}