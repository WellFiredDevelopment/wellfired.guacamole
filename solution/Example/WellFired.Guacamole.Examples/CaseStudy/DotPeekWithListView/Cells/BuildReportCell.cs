using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView.Cells
{
    public class BuildReportCell : Cell
    {
        public BuildReportCell()
        {
            HorizontalLayout = LayoutOptions.Expand;
            VerticalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.Black;
            
            var buildTimeLabel = new Label();
            var platformLabel = new Label();
            var unityVersionLabel = new Label();
            var buildSizeLabel = new Label();

            buildTimeLabel.Bind(Label.TextProperty, "BuildTime");
            platformLabel.Bind(Label.TextProperty, "Platform");
            unityVersionLabel.Bind(Label.TextProperty, "UnityVersion");
            buildSizeLabel.Bind(Label.TextProperty, "BuildSize");
            buildSizeLabel.Bind(BackgroundColorProperty, "BuildSizeBackgroundColor");
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical, 20),
                Children = {
                    new LayoutView
                    {
                        HorizontalLayout = LayoutOptions.Fill,
                        Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                        Children = { new Label { Text = "Build Time : " }, buildTimeLabel }
                    },
                    new LayoutView
                    {
                        HorizontalLayout = LayoutOptions.Fill,
                        Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                        Children = { new Label { Text = "Platform : " }, platformLabel }
                    },
                    new LayoutView
                    {
                        HorizontalLayout = LayoutOptions.Fill,
                        Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                        Children = { new Label { Text = "Unity Version : " }, unityVersionLabel }
                    },
                    new LayoutView
                    {
                        HorizontalLayout = LayoutOptions.Fill,
                        Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                        Children = { new Label { Text = "Build Size : " }, buildSizeLabel }
                    },
                }
            };
        }
    }
}