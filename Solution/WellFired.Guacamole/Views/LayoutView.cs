using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public class LayoutView : Layout
    {
        public ILayoutChildren Layout { private get; set; }

        public override void DoLayout()
        {
            Layout.Layout(Children, Padding, HorizontalLayout, VerticalLayout);
        }

        protected override UIRect CalculateValidRectRequest()
        {
            return Layout.CalculateValidRextRequest(Children, MinSize);
        }

        public override void AttemptToFullfillRequests(UIRect availableSpace)
        {
            base.AttemptToFullfillRequests(availableSpace);
            Layout.AttemptToFullfillRequests(Children, RectRequest, Padding, HorizontalLayout, VerticalLayout);
        }
    }
}