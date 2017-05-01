using System;

namespace WellFired.Guacamole.Attributes
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class CustomRendererAttribute : Attribute
	{
		public CustomRendererAttribute(Type controlType, Type rendererType)
		{
			ControlType = controlType;
			RendererType = rendererType;
		}

		public Type ControlType { get; private set; }

		public Type RendererType { get; private set; }
	}
}