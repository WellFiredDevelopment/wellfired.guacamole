using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class ViewBase
	{
		public IList<ViewBase> Children
		{
			get;
			set;
		}

		public LayoutOptions HorizontalLayout 
		{
			get;
			set;
		}

		public LayoutOptions VerticalLayout 
		{
			get;
			set;
		}

		public UIColor BackgroundColor 
		{
			get;
			set;
		}

		public ViewBase()
		{
			Children = new List<ViewBase>();
		}
	}
}