using System;

namespace WellFired.Guacamole
{
	[System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true)]
	public class CustomRendererAttribute : System.Attribute
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