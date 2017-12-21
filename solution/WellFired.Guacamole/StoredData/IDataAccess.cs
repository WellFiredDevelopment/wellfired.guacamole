namespace WellFired.Guacamole.StoredData
{
	public interface IDataAccess
	{
		void Save(string key);
		void Track(string key, IDataProxy dataProxy);
	}
}