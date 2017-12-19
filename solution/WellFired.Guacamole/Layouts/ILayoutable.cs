using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Layouts
{
    public interface ILayoutable
    {
        float X { get; set; }
        float Y { get; set; }
        UIRect RectRequest { get; set; }
        UIRect ContentRectRequest{ get; set; }
        LayoutOptions HorizontalLayout { get; set; }
        LayoutOptions VerticalLayout { get; set; }
    }
}