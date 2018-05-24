namespace WellFired.Guacamole.DataStorage.Data.Serialization
{
	public interface ISerializer
	{
		string Serialize(object data, bool indented = true);
		T Unserialize<T>(string serializedData) where T : class;
	}
}