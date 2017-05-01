using WellFired.Guacamole.Examples.Taskist.View;
using WellFired.Guacamole.Views;

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