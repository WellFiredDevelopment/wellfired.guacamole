using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class TaskistTopBar : Views.View
    {
        public TaskistTopBar()
        {
            OutlineColor = UIColor.Black;
            HorizontalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.FromRGB(203, 85, 72);
            MinSize = UISize.Of(0, 40);
        }
    }
}