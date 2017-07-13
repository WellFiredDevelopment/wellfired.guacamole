using System.Collections.Generic;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Taskist.View.Cells
{
    public class FilterCell : Cell
    {
        public FilterCell()
        {
            OutlineColor = UIColor.FromRGB(250, 250, 250);
            BackgroundColor = UIColor.FromRGB(250, 250, 250);
            
            var filterColor = new Button {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center,
                MinSize = UISize.Of(10) 
            };
            
            var filterName = new Label {
                OutlineColor = UIColor.FromRGB(250, 250, 250),
                BackgroundColor = UIColor.FromRGB(250, 250, 250),
                TextColor = UIColor.Black,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Center
            };

            filterName.Bind(Label.TextProperty, "FilterName");

            Content = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 10),
                VerticalLayout = LayoutOptions.Center,
                Children = new List<ILayoutable> {
                    filterColor,
                    filterName
                }
            };
        }
    }
}