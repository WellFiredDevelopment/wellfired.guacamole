using WellFired.Guacamole.Layout;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class Overview : AdjacentLayout
    {
        public Overview()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.FromRGB(255, 255, 255);
        }
    }
}