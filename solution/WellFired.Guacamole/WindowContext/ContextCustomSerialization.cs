using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Json;
using WellFired.Json.Serialization;

namespace WellFired.Guacamole.WindowContext
{
	public class ContextCustomSerialization : DefaultContractResolver
	{
		private readonly string[] _invalidProperties;

		public ContextCustomSerialization()
		{
			GetterInfo.GetInfo<UIRect, UILocation>(rec => rec.Location, out var locationProperty, out var _);
			GetterInfo.GetInfo<UIRect, UISize>(rec => rec.Size, out var sizeProperty, out var _);

			_invalidProperties = new[] {locationProperty, sizeProperty};
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var properties = base.CreateProperties(type, memberSerialization);
			properties = properties.Where(p => !_invalidProperties.Contains(p.PropertyName)).ToList();

			return properties;
		}
	}
}