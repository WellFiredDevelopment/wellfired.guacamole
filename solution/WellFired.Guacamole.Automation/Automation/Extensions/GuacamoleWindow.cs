using WellFired.Guacamole.Event;

namespace WellFired.Guacamole.Automation.Extensions
{
    public static class GuacamoleWindow
    {
        public static void RaiseEventFor(this IWindow window, string controlId, IEvent raisedEvent)
        {
            window.MainContent.RaiseEventFor(controlId, raisedEvent);
        }
    }
}