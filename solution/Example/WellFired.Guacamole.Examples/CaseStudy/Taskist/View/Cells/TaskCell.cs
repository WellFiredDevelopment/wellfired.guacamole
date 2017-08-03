using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells
{
    public class TaskCell : Cell
    {
        public TaskCell()
        {
            Style = Styles.FilterCell.Style;
            
            var doneNotDone = new ToggleView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Center
            };

            var filterName = new Label
            {
                BackgroundColor = UIColor.Beige,
                HorizontalTextAlign = UITextAlign.Start,
                VerticalTextAlign = UITextAlign.Middle,
                TextColor = UIColor.FromRGB(62, 62, 62),
                VerticalLayout = LayoutOptions.Fill,
                FontSize = 14
            };
            
            var layoutView = new LayoutView
            {
                BackgroundColor = UIColor.Clear,
                OutlineColor = UIColor.Clear,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 16),
                Children = {
                    doneNotDone,
                    filterName
                }
            };
            
            doneNotDone.Style = default(Style); // Clear the default style on this because we don't want it to act like regular ImageView
            filterName.Style = default(Style);  // Clear the default style on this because we don't want it to act like regular Label
            layoutView.Style = default(Style);  // Clear the default style on this because we don't want it to act like regular LayoutView

            Content = layoutView;
            
            filterName.Bind(Label.TextProperty, "Description");
            filterName.Bind(ToggleView.OnProperty, "Done", BindingMode.TwoWay);
        }
    }
}