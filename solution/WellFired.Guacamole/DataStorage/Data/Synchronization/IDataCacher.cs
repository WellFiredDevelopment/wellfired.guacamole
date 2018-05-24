namespace WellFired.Guacamole.DataStorage.Data.Synchronization
{
	public interface IDataCacher
	{
		string GetData(string key);
		bool DidDataChanged(string key);
		void Cache(string key, IDataProxy dataProxy);
		void UpdateData(string key, string dataContent);
		void ResetDataChanged(string key);
	}
}