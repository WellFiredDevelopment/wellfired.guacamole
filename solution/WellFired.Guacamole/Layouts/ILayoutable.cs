using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    public interface ILayoutable
    {
        int X { get; set; }
        int Y { get; set; }
        UIRect RectRequest { get; set; }
        UIRect ContentRectRequest{ get; set; }
        LayoutOptions HorizontalLayout { get; set; }
        LayoutOptions VerticalLayout { get; set; }
    }
}