namespace WellFired.Guacamole
{
	public class ScrollView : ViewBase 
	{
		public OrientationOptions Orientation
		{
			get;
			set;
		}

		public ScrollView()
		{
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Fill;
			VerticalLayout = LayoutOptions.Fill;
		}
	}
}