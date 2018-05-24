using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Json;
using WellFired.Json.Serialization;

namespace WellFired.Guacamole.WindowContext
{
	/// <summary>
	/// This class has for only purpose to serialize some of the <see cref="Context"/> properties in a custom way.
	/// For example, UIRect location and size has a direct influence on the value X, Y, Width, Height. We don't want
	/// to serialize them.
	/// </summary>
	public class ContextCustomSerialization : DefaultContractResolver
	{
		private readonly string[] _invalidProperties;

		public ContextCustomSerialization()
		{
			GetterInfo.GetInfo<UIRect, UILocation>(rec => rec.Location, out var locationProperty, out _);
			GetterInfo.GetInfo<UIRect, UISize>(rec => rec.Size, out var sizeProperty, out _);

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