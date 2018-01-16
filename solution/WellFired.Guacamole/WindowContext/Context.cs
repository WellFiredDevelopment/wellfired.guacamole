using System;
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
		public UIRect UIRect;
		public bool AllowMultiple;
		
		public Type MainContentType
		{
			set => MainContentTypeString = value.AssemblyQualifiedName;
		}
		
		public Type MainViewModelType
		{
			set => MainViewModelTypeString = value.AssemblyQualifiedName;
		}
	}
}