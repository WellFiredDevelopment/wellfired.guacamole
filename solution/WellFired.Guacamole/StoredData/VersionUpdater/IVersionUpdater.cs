namespace WellFired.Guacamole.StoredData.VersionUpdater
{
	public interface IVersionUpdater
	{
		int VersionNo { get; }
		bool IsCompatibleWithCurrentVersion();
		void UpdatePreviousVersion();
	}
}