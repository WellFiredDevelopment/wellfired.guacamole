using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting.Cells
{
    public class AssetBreakdownCell : Cell
    {
        public AssetBreakdownCell()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Expand;
            BackgroundColor = UIColor.Black;

            var path = new Label
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
            };
            
            var beforeSize = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var afterSize = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };

            path.Bind(Label.TextProperty, "Path");
            beforeSize.Bind(Label.TextProperty, "BeforeSize");
            afterSize.Bind(Label.TextProperty, "AfterSize");
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = {
                    path, 
                    beforeSize,
                    afterSize
                }
            };
        }
    }
}