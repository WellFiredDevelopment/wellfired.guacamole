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

            var path = new LabelView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
            };
            
            var beforeSize = new LabelView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var afterSize = new LabelView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };

            path.Bind(LabelView.TextProperty, "Path");
            beforeSize.Bind(LabelView.TextProperty, "BeforeSize");
            afterSize.Bind(LabelView.TextProperty, "AfterSize");
            
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