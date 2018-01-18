namespace WellFired.Guacamole.DataStorage.Data.Serialization
{
	public interface ISerializer
	{
		string Serialize(object data);
		T Unserialize<T>(string serializedData) where T : class;
	}
}