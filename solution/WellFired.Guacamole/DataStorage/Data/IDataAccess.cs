using WellFired.Guacamole.DataStorage.Data.Synchronization;

namespace WellFired.Guacamole.DataStorage.Data
{
	public interface IDataAccess
	{
		void Save(string key);
		void Track(string key, IDataProxy dataProxy);
	}
}