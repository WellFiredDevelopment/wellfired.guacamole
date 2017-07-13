using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class TaskistTopBar : Views.View
    {
        public TaskistTopBar()
        {
            OutlineColor = UIColor.FromRGB(186, 52, 27);
            BackgroundColor = UIColor.FromRGB(203, 85, 72);
            HorizontalLayout = LayoutOptions.Fill;
            MinSize = UISize.Of(0, 40);
        }
    }
}