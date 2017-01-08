using WellFired.Guacamole.Layout;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class TaskistTopBar : AdjacentLayout
    {
        public TaskistTopBar()
        {
            HorizontalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.FromRGB(203, 85, 72);
            Padding = new UIPadding(0, 20, 0, 20);
        }
    }
}