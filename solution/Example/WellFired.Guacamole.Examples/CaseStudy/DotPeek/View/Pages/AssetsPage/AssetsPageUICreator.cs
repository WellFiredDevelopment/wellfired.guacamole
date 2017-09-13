using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.UIElements;
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

            totalSizeValue.Bind(Label.TextProperty, "TotalSize", BindingMode.ReadOnly);
            totalSizeValue.Bind(Views.View.BackgroundColorProperty, "TotalSizeBacgroundColor", BindingMode.ReadOnly);
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
            var pathHeader = new HeaderButton {Text= "Path", HorizontalLayout = LayoutOptions.Fill, VerticalLayout = LayoutOptions.Fill};
            var importedSizeHeader = new HeaderButton {Text= "Imp. Size", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000)};
            var rawSizeHeader = new HeaderButton {Text= "Raw Size", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000)};
            var percentageHeader = new HeaderButton {Text= "Per.", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000)};
            
            pathHeader.Bind(Button.ButtonPressedCommandProperty, "SortByAssetPath");
            importedSizeHeader.Bind(Button.ButtonPressedCommandProperty, "SortByImportedSize");
            rawSizeHeader.Bind(Button.ButtonPressedCommandProperty, "SortByRawSize");
            percentageHeader.Bind(Button.ButtonPressedCommandProperty, "SortByPercentage");
            
            pathHeader.Bind(Button.TextProperty, "AssetPathText");
            importedSizeHeader.Bind(Button.TextProperty, "ImportedSizeText");
            rawSizeHeader.Bind(Button.TextProperty, "RawSizeText");
            percentageHeader.Bind(Button.TextProperty, "PercentageText");

            var listHeader = LayoutFactory.CreateHorizontalLayout(pathHeader, importedSizeHeader, rawSizeHeader, percentageHeader);
            listHeader.Padding = UIPadding.With(0, 0, 0, 10);
            
            var list = new ListView {
                EntrySize = 14,
                Spacing = 0,
                Orientation = OrientationOptions.Vertical,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                ItemTemplate = DataTemplate.Of(typeof(AssetCell)),
            };
            
            list.Bind(ItemsView.ItemSourceProperty, "DisplayedAssetsList", BindingMode.ReadOnly);

            return LayoutFactory.CreateVerticalLayout(listHeader, list);
        }
    }
}