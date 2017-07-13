using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole
{
	public interface IView
	{
	    IView Content { get; }
	    UIPadding Padding { get; }
	    UIRect RectRequest { get; set; }
		UIRect ContentRectRequest { get; set; }
	    string Id { get; set; }
	    bool ValidRectRequest { get; set; }
	    INativeRenderer NativeRenderer { get; }
	    UISize MinSize { get; }
	    LayoutOptions HorizontalLayout { get; }
	    LayoutOptions VerticalLayout { get; }
	}
}