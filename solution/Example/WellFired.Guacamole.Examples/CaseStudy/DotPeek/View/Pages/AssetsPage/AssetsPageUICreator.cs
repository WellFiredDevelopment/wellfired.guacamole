using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Layout;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.UsedAssetsPage
{
    public static class AssetsPageUICreator
    {
        public static Views.View GenerateTotalSize()
        {
            var totalSizeTitle = new Label
            {
                Text = "Total Size",
                HorizontalTextAlign = UITextAlign.Start,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0)
            };

            var totalSizeValue = new Label
            {
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Center,
                HorizontalLayout = LayoutOptions.Center,
                Padding = UIPadding.With(10, 10, 10, 10),
                FontSize = 30
            };

            totalSizeValue.Bind(Label.TextProperty, "TotalSize");
            totalSizeValue.Bind(Views.View.BackgroundColorProperty, "TotalSizeBacgroundColor");
            var viewContainer = new ViewContainer
            {
                Content = totalSizeValue,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center
            };

            return LayoutFactory.CreateHorizontalLayout(totalSizeTitle, viewContainer);
        }

        public static Views.View GenerateUsedAssetsList()
        {
            return ListViewFactory.CreateListView(typeof(AssetCell), "DisplayedAssetsList",
                new ListViewFactory.LegendDefinition("Path", UISize.Min, UISize.Max),
                new ListViewFactory.LegendDefinition("Imported Size", UISize.Of(80, 0), UISize.Max),
                new ListViewFactory.LegendDefinition("Raw Size", UISize.Of(80, 0), UISize.Max),
                new ListViewFactory.LegendDefinition("Percentage", UISize.Of(80, 0), UISize.Max));
        }
    }
}