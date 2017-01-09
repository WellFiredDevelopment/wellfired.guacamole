using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class Inspector : Views.View
    {
        public Inspector()
        {
            OutlineColor = UIColor.Black;
            MinSize = new UISize(300, 0);
            VerticalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.FromRGB(250, 250, 250);
        }
    }
}