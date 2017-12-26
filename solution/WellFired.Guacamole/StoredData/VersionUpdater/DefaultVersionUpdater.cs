using WellFired.Guacamole.Platform;
using WellFired.Guacamole.StoredData.Serialization;

namespace WellFired.Guacamole.StoredData.VersionUpdater
{
	public abstract class DefaultVersionUpdater : IVersionUpdater
	{
		private const string VersionLocation = "Version";
		private readonly ISerializer _serializer;
		private readonly IDataStorageService _storageService;

		public abstract int VersionNo { get; }

		protected DefaultVersionUpdater(ISerializer serializer, IDataStorageService storageService)
		{
			_serializer = serializer;
			_storageService = storageService;
		}

		public bool IsCompatibleWithCurrentVersion()
		{
			return _serializer.Unserialize<Version>(_storageService.Read(VersionLocation)).VersionNo == VersionNo;
		}

		public abstract void UpdatePreviousVersion();
	}
}