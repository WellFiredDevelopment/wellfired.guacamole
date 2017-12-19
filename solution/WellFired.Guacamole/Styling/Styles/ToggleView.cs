using WellFired.Guacamole.Data;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class ToggleView
    {
        private static readonly IImageSource OnImageSource = ImageSource.From(ImageShape.Circle, 4.0, UIColor.Grey);
        private static readonly IImageSource OffImageSource = ImageSource.From(ImageShape.Circle, 4.0, UIColor.Clear, UIColor.Grey);
        
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Expand},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
                new Setter {Property = Views.ToggleView.OnImageSourceProperty, Value = OnImageSource},
                new Setter {Property = Views.ToggleView.OffImageSourceProperty, Value = OffImageSource}
            }
        };
    }
}