using WellFired.Guacamole.Examples.Taskist.View;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.Taskist
{
    public class TaskistWindow : Window
    {
        public TaskistWindow()
        {
            Content = new TaskistMainPage();
        }
    }
}