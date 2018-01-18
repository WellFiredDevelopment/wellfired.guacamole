using System;
using WellFired.Guacamole.Diagnostics;
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
			string serializedData = null;
			try
			{
				serializedData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto,
					DateTimeZoneHandling = DateTimeZoneHandling.Utc,
					ContractResolver = _contractResolver
				});
			}
			catch (Exception e)
			{
				Logger.LogError($"An error happened while serializing the object : {e.Message}\n{e.StackTrace}");
			}

			return serializedData;
		}

		public T Unserialize<T>(string serializedData) where T : class
		{
			T unserializedData = null;
			try
			{
				unserializedData = JsonConvert.DeserializeObject<T>(serializedData, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto
				});
			}
			catch (Exception e)
			{
				Logger.LogError($"An error happened while unserializing the object : {e.Message}\n{e.StackTrace}");
			}
			
			return unserializedData;
		}
	}
}