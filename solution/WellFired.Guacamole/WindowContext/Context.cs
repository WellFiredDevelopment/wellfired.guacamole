using System;
using System.Linq;
using System.Reflection;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.WindowContext
{
	public class Context
	{
		public string MainContentTypeString;
		public string MainViewModelTypeString;
		public UISize MaxSize;
		public UISize MinSize;
		public string Title;
		public string ApplicationName;
		public string CompanyName;
		public UIRect UIRect;
		public bool AllowMultiple;
		public string[] ExternalRenderersAssembliesStrings;
		
		public Type MainContentType
		{
			set => MainContentTypeString = value.AssemblyQualifiedName;
		}
		
		public Type MainViewModelType
		{
			set => MainViewModelTypeString = value.AssemblyQualifiedName;
		}

		public Assembly[] ExternalRendererAssemblies
		{
			set => ExternalRenderersAssembliesStrings = value?.Select(assembly => assembly.GetName().Name).ToArray();
		}
	}
}