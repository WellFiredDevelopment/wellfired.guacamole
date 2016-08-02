using System;

namespace WellFired.Guacamole
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class CustomRendererAttribute : Attribute
	{
		public Type ControlType
		{
			get;
			set;
		}

		public Type RendererType
		{
			get;
			set;
		}
	
		public CustomRendererAttribute(Type controlType, Type rendererType)
		{
			ControlType = controlType;
			RendererType = rendererType;
		}
	}
}