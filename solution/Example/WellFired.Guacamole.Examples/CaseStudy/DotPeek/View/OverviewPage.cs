using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View
{
    public class OverviewPage : Page
    {
        public OverviewPage()
        {
            var buildTime = new Label
            {
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(20, 50, 0, 0)
            };
            
            var commitId = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 50, 0, 0)
            };
            
            buildTime.Bind(Label.TextProperty, "BuildTime");
            commitId.Bind(Label.TextProperty, "CommitID");
            var firstLine = Layout.LayoutFactory.CreateHorizontalLayout(buildTime, commitId);
            
            var platform = new Label
            {
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(20, 20, 0, 0)
            };
            
            platform.Bind(Label.TextProperty, "Platform");
            var secondLine = Layout.LayoutFactory.CreateHorizontalLayout(platform, new ViewContainer());
            
            var unityVersionTitle = new Label
            {
                Text = "Unity Version",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(20, 10, 0, 0)
            };

            var unityVersionValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 10, 10, 10),
                FontSize = 30
            };

            unityVersionValue.Bind(Label.TextProperty, "UnityVersion");
            unityVersionValue.Bind(BackgroundColorProperty, "UnityVersionBackgroundColor");
            var viewContainer = new ViewContainer
            {
                Content = unityVersionValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center
            };
            var thirdLine = Layout.LayoutFactory.CreateHorizontalLayout(unityVersionTitle, viewContainer);
            thirdLine.MinSize = UISize.Of(0, 100);
            
            var buildSizeTitle = new Label
            {
                Text = "Build Size",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(20, 10, 0, 0)
            };

            var buildSizeValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 10, 10, 10),
                FontSize = 30
            };

            buildSizeValue.Bind(Label.TextProperty, "BuildSize");
            buildSizeValue.Bind(BackgroundColorProperty, "BuildSizeBackgroundColor");
            var viewContainer2 = new ViewContainer
            {
                Content = buildSizeValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center
            };
            var fourthLine = Layout.LayoutFactory.CreateHorizontalLayout(buildSizeTitle, viewContainer2);
            thirdLine.MinSize = UISize.Of(0, 100);
            
            var verticalLayout = Layout.LayoutFactory.CreateVerticalLayout(firstLine, secondLine, thirdLine, fourthLine);
            Content = verticalLayout;

            BackgroundColor = UIColor.FromRGB(40, 40, 40);
        }
    }
}