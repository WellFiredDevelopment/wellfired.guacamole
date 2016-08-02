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
		
		private static Dictionary<Type, ConstructorInfo> _typeMap;
		
		public static INativeRenderer CreateNativeRendererFor(Type controlType)
		{
			if(_typeMap == null) 
			{
				var attributes = LaunchedAssembly
					.GetCustomAttributes(typeof(CustomRendererAttribute), false)
					.Cast<CustomRendererAttribute>();
				
				_typeMap = new Dictionary<Type, ConstructorInfo> ();
				foreach(var attribute in attributes)
					_typeMap[attribute.ControlType] = attribute.RendererType.GetConstructor(Type.EmptyTypes);
			}

			var checkType = controlType;
			while(!_typeMap.ContainsKey(checkType)) 
			{
				checkType = checkType.BaseType;
				if (checkType == null)
					break;
			}

			return _typeMap[checkType].Invoke(Type.EmptyTypes) as INativeRenderer;
		}
	}
}