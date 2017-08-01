using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View
{
    public class TaskistTopBar : Views.View
    {
        public TaskistTopBar()
        {
            OutlineColor = UIColor.FromRGB(186, 52, 27);
            BackgroundColor = UIColor.FromRGB(216, 80, 68);
            HorizontalLayout = LayoutOptions.Fill;
            MinSize = UISize.Of(0, 40);
        }
    }
}