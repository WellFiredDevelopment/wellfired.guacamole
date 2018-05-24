namespace WellFired.Guacamole.DataStorage.Data.VersionUpdater
{
	public interface IVersionUpdater
	{
		int VersionNo { get; }
		bool IsCompatibleWithCurrentVersion();
		void UpdatePreviousVersion();
	}
}