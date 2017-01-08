using WellFired.Guacamole.Layout;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class Inspector : AdjacentLayout
    {
        public Inspector()
        {
            MinSize = new UISize(300, 0);
            VerticalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.FromRGB(250, 250, 250);
        }
    }
}