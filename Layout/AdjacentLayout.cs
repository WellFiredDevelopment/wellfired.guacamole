namespace WellFired.Guacamole
{
	public class AdjacentLayout : ViewBase 
	{
		public OrientationOptions Orientation
		{
			get;
			set;
		}

		public AdjacentLayout()
		{
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}
	}
}