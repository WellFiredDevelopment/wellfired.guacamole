using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells
{
    public class FilterCell : Cell
    {
        public FilterCell()
        {
            Style = Styles.FilterCell.Style;
            
            var filterBall = new ImageView
            {
                BackgroundColor = UIColor.Clear,
                OutlineColor = UIColor.Clear,
                MinSize = UISize.Of(12),
                MaxSize = UISize.Of(12),
                HorizontalLayout = LayoutOptions.Expand,
            };

            var filterName = new Label
            {
                BackgroundColor = UIColor.Clear,
                OutlineColor = UIColor.Clear,
                HorizontalTextAlign = UITextAlign.Start,
                TextColor = UIColor.FromRGB(62, 62, 62),
                VerticalLayout = LayoutOptions.Center,
                FontSize = 14
            };
            
            var layoutView = new LayoutView
            {
                BackgroundColor = UIColor.Clear,
                OutlineColor = UIColor.Clear,
                Padding = UIPadding.With(10, 0, 0, 0),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 16),
                Children = {
                    filterBall,
                    filterName
                }
            };
            
            filterBall.Style = default(Style); // Clear the default style on this because we don't want it to act like regular ImageView
            filterName.Style = default(Style); // Clear the default style on this because we don't want it to act like regular Label
            layoutView.Style = default(Style); // Clear the default style on this because we don't want it to act like regular LayoutView

            Content = layoutView;
            
            filterBall.Bind(ImageView.ImageSourceProperty, "FilterImage");
            filterName.Bind(Label.TextProperty, "FilterName");
        }
    }
}