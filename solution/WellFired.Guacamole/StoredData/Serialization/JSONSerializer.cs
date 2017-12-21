using System;
using Newtonsoft.Json;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.StoredData.Serialization
{
	public class JSONSerializer : ISerializer
	{
		public string Serialize(object data)
		{
			string serializedData = null;
			try
			{
				serializedData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Objects,
					TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
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
			try
			{
				return JsonConvert.DeserializeObject<T>(serializedData, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Objects
				});
			}
			catch (Exception e)
			{
				Logger.LogError($"An error happened while serializing the object : {e.Message}\n{e.StackTrace}");
				throw;
			}
		}
	}
}