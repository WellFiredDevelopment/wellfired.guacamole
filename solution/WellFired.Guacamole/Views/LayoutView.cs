using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
	public partial class LayoutView : ViewWithChildren, ICanLayout
	{
	    public LayoutView()
		{
		    OutlineColor = UIColor.Clear;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}
	}
}