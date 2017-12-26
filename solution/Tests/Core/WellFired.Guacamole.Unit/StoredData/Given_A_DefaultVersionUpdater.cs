using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.StoredData.Serialization;
using WellFired.Guacamole.StoredData.VersionUpdater;

namespace WellFired.Guacamole.Unit.StoredData
{
	[TestFixture]
	public class Given_A_DefaultVersionUpdater
	{
		[Test]
		public void Test_Version_Compatibility()
		{
			var serializer = Substitute.For<ISerializer>();
			var storageService = Substitute.For<IDataStorageService>();

			storageService.Read("Version").Returns("{VersionNo:3}");
			serializer.Unserialize<Version>("{VersionNo:3}").Returns(new Version {VersionNo = 3});
			
			var versionUpdater3 = new VersionUpdater3(serializer, storageService);
			
			Assert.IsTrue(versionUpdater3.IsCompatibleWithCurrentVersion());
		}
		
		[Test]
		public void Test_Version_Incompatibility()
		{
			var serializer = Substitute.For<ISerializer>();
			var storageService = Substitute.For<IDataStorageService>();

			storageService.Read("Version").Returns("{VersionNo:4}");
			serializer.Unserialize<Version>("{VersionNo:4}").Returns(new Version {VersionNo = 4});
			
			var versionUpdater3 = new VersionUpdater3(serializer, storageService);
			
			Assert.IsFalse(versionUpdater3.IsCompatibleWithCurrentVersion());
		}
		
		private class VersionUpdater3 : DefaultVersionUpdater
		{
			public VersionUpdater3(ISerializer serializer, IDataStorageService storageService) : base(serializer, storageService)
			{
			}

			public override int VersionNo => 3;

			public override void UpdatePreviousVersion()
			{
				throw new System.NotImplementedException();
			}
		}
	}
}