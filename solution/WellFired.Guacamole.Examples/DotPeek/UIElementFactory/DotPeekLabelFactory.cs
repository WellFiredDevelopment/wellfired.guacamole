using WellFired.Guacamole.Examples.DotPeek.Layout;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeek.UIElementFactory
{
    public static class DotPeekLabelFactory
    {   
        public static LayoutView Create(string label, string value, string binding = "")
        {
            var leftLabel = new Label
            {
                Text = label,
                Style = Styles.Label.Style,
                HorizontalTextAlign = UITextAlign.End,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill
            };

            var rightLabel = new Label
            {
                Text = value,
                Style = Styles.Label.Style,
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill
            };

            var leftViewContainer = new ViewContainer
            {
                Content = leftLabel
            };

            var rightViewContainer = new ViewContainer
            {
                Content = rightLabel
            };

            if (!string.IsNullOrEmpty(binding))
                rightLabel.Bind(View.BackgroundColorProperty, binding);

            return LayoutFactory.CreateHorizontalLayout(leftViewContainer, rightViewContainer);
        }
    }
}