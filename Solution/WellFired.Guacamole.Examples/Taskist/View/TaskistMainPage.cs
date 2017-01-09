using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class TaskistMainPage : Page
    {
        public TaskistMainPage()
        {
            Content = new AdjacentLayout
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children =
                {
                    new TaskistTopBar(),
                    new AdjacentLayout
                    {
                        Orientation = OrientationOptions.Horizontal,
                        HorizontalLayout = LayoutOptions.Fill,
                        VerticalLayout = LayoutOptions.Fill,
                        Children =
                        {
                            new Inspector(),
                            new Overview()
                        }
                    }
                }
            };
        }
    }
}