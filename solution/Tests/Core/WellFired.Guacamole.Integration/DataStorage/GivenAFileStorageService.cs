using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Storages;
using WellFired.Guacamole.DataStorage.Synchronization;

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

		[Test]
		public void When_Read_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			ThreadSynchronizer.InitializeSharedInstance(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();

			//we try to save in an unexisting location to throw exception and ensure read lock still get released.
			var fileStorageService = new FileStorageService($"{dllPath}/an/unexisting/path");

			try
			{
				fileStorageService.Read("aKey");
			}
			catch (Exception)
			{
				Assert.That(() => keyBasedLocker.Received(1).EnterReadLock($"{dllPath}/an/unexisting/path" + "aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitReadLock($"{dllPath}/an/unexisting/path" + "aKey"), Throws.Nothing);
			}
		}

		[Test]
		public void When_Write_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			ThreadSynchronizer.InitializeSharedInstance(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();

			//we try to save in an unexisting location to throw exception and ensure read lock still get released.
			var fileStorageService = new FileStorageService($"{dllPath}/an/unexisting/path");

			try
			{
				fileStorageService.Write("Cow", "aKey");
			}
			catch (Exception)
			{
				Assert.That(() => keyBasedLocker.Received(1).EnterWriteLock($"{dllPath}/an/unexisting/path" + "aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitWriteLock($"{dllPath}/an/unexisting/path" + "aKey"), Throws.Nothing);
			}
		}

		[Test]
		public void When_Check_Exists_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			ThreadSynchronizer.InitializeSharedInstance(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();
			var fileStorageService = new FileStorageService($"{dllPath}");

			try
			{
				fileStorageService.Exists("aKey");
			}
			catch (Exception)
			{
				Assert.That(() => keyBasedLocker.Received(1).EnterReadLock($"{dllPath}aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitReadLock($"{dllPath}aKey"), Throws.Nothing);
			}
		}

		[Test]
		public void When_Delete_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			ThreadSynchronizer.InitializeSharedInstance(keyBasedLocker, true);

			var dllPath = Utils.GetTestDllRepository();

			//we try to save in an unexisting location to throw exception and ensure read lock still get released.
			var fileStorageService = new FileStorageService($"{dllPath}/an/unexisting/path");

			try
			{
				fileStorageService.Delete("aKey");
			}
			catch (Exception)
			{
				Assert.That(() => keyBasedLocker.Received(1).EnterWriteLock($"{dllPath}/an/unexisting/path" + "aKey"), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitWriteLock($"{dllPath}/an/unexisting/path" + "aKey"), Throws.Nothing);
			}
		}
	}
}