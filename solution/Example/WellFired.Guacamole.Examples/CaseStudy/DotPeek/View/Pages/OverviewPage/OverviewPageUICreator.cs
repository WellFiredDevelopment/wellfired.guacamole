using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.UIElements;
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
                HorizontalLayout = LayoutOptions.Expand
            };
            
            var commitId = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0,8,0,0)
            };
            
            buildTime.Bind(Label.TextProperty, "BuildTime", BindingMode.ReadOnly);
            commitId.Bind(Label.TextProperty, "CommitID", BindingMode.ReadOnly);
            
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
            
            platform.Bind(Label.TextProperty, "Platform", BindingMode.ReadOnly);
            return LayoutFactory.CreateHorizontalLayout(platform);
        }

        public static Views.View GenerateUnityVersion()
        {
            var unityVersionTitle = new Label
            {
                Text = "<b>Unity Version</b>",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0),
                FontSize = 25
            };

            var unityVersionValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 10, 10, 10),
                FontSize = 20
            };

            unityVersionValue.Bind(Label.TextProperty, "UnityVersion", BindingMode.ReadOnly);
            unityVersionValue.Bind(Views.View.BackgroundColorProperty, "UnityVersionBackgroundColor", BindingMode.ReadOnly);
            var viewContainer = new ViewContainer
            {
                Content = unityVersionValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(0, 8, 0, 0)
            };
            
            return LayoutFactory.CreateHorizontalLayout(unityVersionTitle, viewContainer);
        }

        public static Views.View GenerateBuildSize()
        {
            var buildSizeTitle = new Label
            {
                Text = "<b>Build Size</b>",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0),
                FontSize = 30
            };

            var buildSizeValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 13, 10, 10),
                FontSize = 30
            };

            buildSizeValue.Bind(Label.TextProperty, "BuildSize", BindingMode.ReadOnly);
            buildSizeValue.Bind(Views.View.BackgroundColorProperty, "BuildSizeBackgroundColor", BindingMode.ReadOnly);
            var viewContainer = new ViewContainer
            {
                Content = buildSizeValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(0, 5, 0, 0)
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
            
            resourcesAssetSizeValue.Bind(Label.TextProperty, "ResourceAssetsSize", BindingMode.ReadOnly);
            resourcesAssetSizeValue.Bind(Views.View.BackgroundColorProperty, "ResourceAssetsSizeBackgroundColor", BindingMode.ReadOnly);
            
            return LayoutFactory.CreateHorizontalLayout(resourcesAssetSizeTitle, viewContainer);
        }

        public static Views.View GenerateBuildAssetSplit()
        {
            var buildAssetSplit = new Label
            {
                Text = "Asset Breakdown",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Expand,
                HorizontalLayout = LayoutOptions.Expand,
                FontSize = 18,
                Padding = UIPadding.With(0, 20, 0, 0)
            };
            
            var list = new ListView {
                EntrySize = 24,
                Orientation = OrientationOptions.Vertical,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                ItemTemplate = DataTemplate.Of(typeof(AssetSplitCell)),
            };
            
            list.Bind(ItemsView.ItemSourceProperty, "AssetSplitList", BindingMode.ReadOnly);

            return LayoutFactory.CreateVerticalLayout(buildAssetSplit, list);
        }
    }
}