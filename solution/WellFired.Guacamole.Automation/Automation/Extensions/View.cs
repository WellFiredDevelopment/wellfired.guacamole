using WellFired.Guacamole.Event;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Automation.Extensions
{
    public static class View
    {
        public static void RaiseEventFor(this Views.View view, string controlId, IEvent raisedEvent)
        {
            if (view == null)
                return;
            
            HandleWithoutChildren(view, controlId, raisedEvent);
            RaiseEventFor((Views.View)view.Content, controlId, raisedEvent);
            
            var hasChildren = view as IHasChildren;
            if (hasChildren != null)
                HandleWithChildren(hasChildren, controlId, raisedEvent);
        }

        private static void HandleWithoutChildren(Views.View view, string controlId, IEvent raisedEvent)
        {
            var viewId = view.Id;
            if (viewId == controlId)
                view.RaiseEvent(raisedEvent);
        }

        private static void HandleWithChildren(IHasChildren hasChildren, string controlId, IEvent raisedEvent)
        {
            foreach (var child in hasChildren.Children)
                RaiseEventFor((Views.View)child, controlId, raisedEvent);
        }
    }
}