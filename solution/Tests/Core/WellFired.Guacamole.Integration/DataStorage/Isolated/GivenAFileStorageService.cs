using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Synchronization;
using WellFired.Guacamole.DataStorage.Types;

namespace WellFired.Guacamole.Integration.DataStorage.Isolated
{
	[TestFixture]
	public class GivenAFileStorageService
	{
		[Test]
		public void When_Instantiate_With_Non_Existent_Directory_Then_Create_Directory_And_Successfully_Save_Data_Inside()
		{
			var fileStorageService = new IsolatedFileStorageService("NonExistentFolder");
			try
			{
				fileStorageService.Write("some data", "Options");
				Assert.That(fileStorageService.Read("Options"), Is.EqualTo("some data"));
			}
			finally
			{
				fileStorageService.Clear();
			}
		}

		[Test]
		public void When_File_Does_Not_Exist_Then_Return_False()
		{
			var fileStorageService = new IsolatedFileStorageService("storage");
			Assert.IsFalse(fileStorageService.Exists("Options"));
		}

		[Test]
		public void When_File_Exist_Then_Return_True()
		{
			var fileStorageService = new IsolatedFileStorageService("storage");

			try
			{
				fileStorageService.Write("some data", "Options");
				Assert.IsTrue(fileStorageService.Exists("Options"));
			}
			finally
			{
				fileStorageService.Clear();
			}
		}

		[Test]
		public void When_Read_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			IsolatedFileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var fileStorageService = new IsolatedFileStorageService("storage");

			var exceptionNotThrown = false;
			try
			{
				fileStorageService.Read("aKey");
				exceptionNotThrown = true;
			}
			catch (Exception)
			{
				Assert.That(() => keyBasedLocker.Received(1).EnterReadLock(Path.Combine("storage", "aKey")), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitReadLock(Path.Combine("storage", "aKey")), Throws.Nothing);
			}
			finally
			{
				fileStorageService.Clear();
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
			IsolatedFileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var fileStorageService = new IsolatedFileStorageService("storage");
			
			try
			{
				fileStorageService.Write("Cow", "aKey");
				Assert.That(() => keyBasedLocker.Received(1).EnterWriteLock(Path.Combine("storage", "aKey")), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(1).ExitWriteLock(Path.Combine("storage", "aKey")), Throws.Nothing);
			}
			finally
			{
				fileStorageService.Clear();
			}
		}

		[Test]
		public void When_Check_Exists_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			IsolatedFileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var fileStorageService = new IsolatedFileStorageService("storage");

			fileStorageService.Exists("aKey");

			Assert.That(() => keyBasedLocker.Received(1).EnterReadLock(Path.Combine("storage", "aKey")), Throws.Nothing);
			Assert.That(() => keyBasedLocker.Received(1).ExitReadLock(Path.Combine("storage", "aKey")), Throws.Nothing);
		}

		[Test]
		public void When_Delete_Then_Synchronization_Is_Correct()
		{
			var keyBasedLocker = Substitute.For<IKeyBasedReadWriteLock>();
			IsolatedFileStorageService.InitializeSharedThreadLock(keyBasedLocker, true);

			var fileStorageService = new IsolatedFileStorageService("storage");
			
			try
			{
				fileStorageService.Write("test", "aKey");
				fileStorageService.Delete("aKey");
				
				Assert.That(() => keyBasedLocker.Received(2).EnterWriteLock(Path.Combine("storage", "aKey")), Throws.Nothing);
				Assert.That(() => keyBasedLocker.Received(2).ExitWriteLock(Path.Combine("storage", "aKey")), Throws.Nothing);
			}
			finally
			{
				fileStorageService.Clear();
			}
		}
	}
}