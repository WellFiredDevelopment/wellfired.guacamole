namespace WellFired.Guacamole.StoredData.Serialization
{
	public interface ISerializer
	{
		string Serialize(object data);
		T Unserialize<T>(string serializedData) where T : class;
	}
}