using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace WellFired.Guacamole
{
	public static class NativeRendererHelper
	{
		public static Assembly LaunchedAssembly 
		{
			get;
			set;
		}
		
		private static Dictionary<Type, ConstructorInfo> TypeMap;
		
		public static INativeRenderer CreateNativeRendererFor(Type controlType)
		{
			if(TypeMap == null) 
			{
				var attributes = LaunchedAssembly
					.GetCustomAttributes(typeof(CustomRendererAttribute), false)
					.Cast<CustomRendererAttribute>();
				
				TypeMap = new Dictionary<Type, ConstructorInfo> ();
				foreach(var attribute in attributes)
					TypeMap[attribute.ControlType] = attribute.RendererType.GetConstructor(Type.EmptyTypes);
			}

			var checkType = controlType;
			while(!TypeMap.ContainsKey(checkType)) 
			{
				checkType = checkType.BaseType;
				if (checkType == null)
					break;
			}

			return TypeMap[checkType].Invoke(Type.EmptyTypes) as INativeRenderer;
		}
	}
}