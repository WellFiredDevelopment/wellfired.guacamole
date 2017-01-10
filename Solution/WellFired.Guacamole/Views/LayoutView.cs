using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public class LayoutView : Layout
    {
        public ILayoutChildren Layout { get; set; }

        public override void DoLayout()
        {
            Layout.Layout(Children, Padding, HorizontalLayout, VerticalLayout);
        }

        protected override UIRect CalculateValidRectRequest()
        {
            return Layout.CalculateValidRextRequest(Children, MinSize);
        }
    }
}