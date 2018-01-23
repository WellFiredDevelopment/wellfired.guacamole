using WellFired.Json;
using WellFired.Json.Serialization;

namespace WellFired.Guacamole.DataStorage.Data.Serialization
{
	public class JSONSerializer : ISerializer
	{
		private readonly IContractResolver _contractResolver;

		public JSONSerializer(){}
		
		public JSONSerializer(IContractResolver contractResolver) : this()
		{
			_contractResolver = contractResolver;
		}

		public string Serialize(object data)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto,
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				ContractResolver = _contractResolver
			});

			return serializedData;
		}

		public T Unserialize<T>(string serializedData) where T : class
		{
			var unserializedData = JsonConvert.DeserializeObject<T>(serializedData, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Auto
			});

			return unserializedData;
		}
	}
}