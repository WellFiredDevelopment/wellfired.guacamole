using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

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