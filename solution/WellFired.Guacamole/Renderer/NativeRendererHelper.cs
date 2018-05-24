using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WellFired.Guacamole.Attributes;

namespace WellFired.Guacamole.Renderer
{
	public static class NativeRendererHelper
	{
		public static Assembly LaunchedAssembly { private get; set; }
		
		private static readonly Dictionary<Type, ConstructorInfo> TypeMap = new Dictionary<Type, ConstructorInfo>();
		private static readonly List<string> AssemblyImported = new List<string>();
		private static bool _defaultAttributesLoaded;

		public static void ImportExternalRenderers(Assembly assembly)
		{
			if (AssemblyImported.Contains(assembly.FullName))
				return;
			
			AssemblyImported.Add(assembly.FullName);
			
			var externalAttributes = assembly
				.GetCustomAttributes(typeof(CustomRendererAttribute), false)
				.Cast<CustomRendererAttribute>();

			foreach (var attribute in externalAttributes)
			{
				TypeMap[attribute.ControlType] = attribute.RendererType.GetConstructor(Type.EmptyTypes);
			}
		}

		public static INativeRenderer CreateNativeRendererFor(Type controlType)
		{
			if (!_defaultAttributesLoaded)
			{
				// This is shit.
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				var exampleAssembly = assemblies.FirstOrDefault(o => o.FullName.Contains("WellFired.Guacamole.Examples.Unity.Editor"));
				
				var launchedAssemblyAttributes = LaunchedAssembly
					.GetCustomAttributes(typeof(CustomRendererAttribute), false)
					.Cast<CustomRendererAttribute>();
				
				var exampleAttributes = exampleAssembly?
					.GetCustomAttributes(typeof(CustomRendererAttribute), false)
					.Cast<CustomRendererAttribute>();

				foreach (var attribute in launchedAssemblyAttributes)
					TypeMap[attribute.ControlType] = attribute.RendererType.GetConstructor(Type.EmptyTypes);
				
				if (exampleAttributes != null)
					foreach (var attribute in exampleAttributes)
						TypeMap[attribute.ControlType] = attribute.RendererType.GetConstructor(Type.EmptyTypes);

				_defaultAttributesLoaded = true;
			}

			var checkType = controlType;
			while (!TypeMap.ContainsKey(checkType))
			{
				checkType = checkType.BaseType;
				if (checkType == null)
					break;
			}

			// ReSharper disable once CoVariantArrayConversion
			// ReSharper disable once AssignNullToNotNullAttribute
			return TypeMap[checkType].Invoke(Type.EmptyTypes) as INativeRenderer;
		}
	}
}