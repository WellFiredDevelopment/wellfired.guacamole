using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeek.UIElementFactory
{
    public static class DotPeekLabelFactory
    {
        public static LayoutView Create(string label, string value, string binding = "")
        {
            var leftLabel = new Label {Text = label, Style = Styles.Label.Style};
            var rightLabel = new Label {Text = value, Style = Styles.Label.Style};
     
            if(!string.IsNullOrEmpty(binding))
                rightLabel.Bind(View.BackgroundColorProperty, binding);

            return new LayoutView
            {
                BackgroundColor = UIColor.Grey,
                Layout = new AdjacentLayout {Orientation = OrientationOptions.Horizontal},
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children = {leftLabel, rightLabel}
            };
        }
    }
}