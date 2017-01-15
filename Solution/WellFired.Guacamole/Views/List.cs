using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public class List : ItemsView
    {
        private ILayoutChildren Layout { get; }

        public List()
        {
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;
            Layout = new AdjacentLayout {Orientation = OrientationOptions.Vertical};
        }

        protected override ICell CreateDefault(object item)
        {
            string text = null;
            if (item != null)
                text = item.ToString ();

            return new LabelCell {
                Text = text
            };
        }

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