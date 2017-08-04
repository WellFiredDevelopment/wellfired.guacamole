using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Layout;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.OverviewPage
{
    public static class OverviewPageUICreator
    {
        public static Views.View GenerateBuildTimeAndCommitID()
        {
            var buildTime = new Label
            {
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill
            };
            
            var commitId = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
            };
            
            buildTime.Bind(Label.TextProperty, "BuildTime");
            commitId.Bind(Label.TextProperty, "CommitID");
            
            return LayoutFactory.CreateHorizontalLayout(buildTime, commitId);
        }

        public static Views.View GeneratePlatform()
        {
            var platform = new Label
            {
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 20, 0, 0)
            };
            
            platform.Bind(Label.TextProperty, "Platform");
            return LayoutFactory.CreateHorizontalLayout(platform, new ViewContainer());
        }

        public static Views.View GenerateUnityVersion()
        {
            var unityVersionTitle = new Label
            {
                Text = "Unity Version",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0)
            };

            var unityVersionValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 10, 10, 10),
                FontSize = 20
            };

            unityVersionValue.Bind(Label.TextProperty, "UnityVersion");
            unityVersionValue.Bind(Views.View.BackgroundColorProperty, "UnityVersionBackgroundColor");
            var viewContainer = new ViewContainer
            {
                Content = unityVersionValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center
            };
            
            return LayoutFactory.CreateHorizontalLayout(unityVersionTitle, viewContainer);
        }

        public static Views.View GenerateBuildSize()
        {
            var buildSizeTitle = new Label
            {
                Text = "Build Size",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0)
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
            buildSizeValue.Bind(Views.View.BackgroundColorProperty, "BuildSizeBackgroundColor");
            var viewContainer = new ViewContainer
            {
                Content = buildSizeValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center
            };
            
            return LayoutFactory.CreateHorizontalLayout(buildSizeTitle, viewContainer);
        }

        public static Views.View GenerateResourceAssetsSize()
        {
            var resourcesAssetSizeTitle = new Label
            {
                Text = "Resources Asset Size",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0)
            };
            
            var resourcesAssetSizeValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 10, 10, 10),
                FontSize = 30
            };
            
            var viewContainer = new ViewContainer
            {
                Content = resourcesAssetSizeValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center
            };
            
            resourcesAssetSizeValue.Bind(Label.TextProperty, "ResourceAssetsSize");
            resourcesAssetSizeValue.Bind(Views.View.BackgroundColorProperty, "ResourceAssetsSizeBackgroundColor");
            
            return LayoutFactory.CreateHorizontalLayout(resourcesAssetSizeTitle, viewContainer);
        }

        public static Views.View GenerateBuildAssetSplit()
        {
            var buildAssetSplit = new Label
            {
                Text = "Build Asset Split",
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(0, 20, 0, 15)
            };
            
            var listView = ListViewFactory.CreateListView(typeof(AssetSplitCell), "AssetSplitList",
                new[]
                {
                    new ListViewFactory.LegendDefinition("Category", UISize.Min, UISize.Max),
                    new ListViewFactory.LegendDefinition("Size", UISize.Of(80, 0), UISize.Max),
                    new ListViewFactory.LegendDefinition("Percentage", UISize.Of(80, 0), UISize.Max)
                });

            return LayoutFactory.CreateVerticalLayout(buildAssetSplit, listView);
        }
    }
}