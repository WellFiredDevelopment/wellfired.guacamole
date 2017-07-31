using WellFired.Guacamole.Event;

namespace WellFired.Guacamole.Automation.Extensions
{
    public static class Application
    {
        public static void RaiseEventFor(this IApplication application, string controlId, IEvent raisedEvent)
        {
            application.MainWindow.RaiseEventFor(controlId, raisedEvent);
        }
    }
}