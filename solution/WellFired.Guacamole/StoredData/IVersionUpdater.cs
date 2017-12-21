namespace WellFired.Guacamole.StoredData
{
	public interface IVersionUpdater
	{
		int VersionNo { get; }
		bool IsCompatibleWithCurrentVersion();
		void UpdatePreviousVersion();
	}
}