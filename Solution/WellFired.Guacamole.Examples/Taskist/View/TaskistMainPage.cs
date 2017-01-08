using System.Collections.Generic;
using WellFired.Guacamole.Layout;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class TaskistMainPage : Page
    {
        public TaskistMainPage()
        {
            Children = new List<ViewBase> {
                new AdjacentLayout {
                    HorizontalLayout = LayoutOptions.Fill,
                    VerticalLayout = LayoutOptions.Fill,
                    Children = {
                        new TaskistTopBar(),
                        new AdjacentLayout {
                            Orientation = OrientationOptions.Horizontal,
                            HorizontalLayout = LayoutOptions.Fill,
                            VerticalLayout = LayoutOptions.Fill,
                            Children = {
                                new Inspector(),
                                new Overview()
                            }
                        }
                    }
                }
            };
        }
    }
}