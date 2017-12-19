using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;
using LayoutView = WellFired.Guacamole.Views.LayoutView;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Cells
{
    public class AssetCell : Cell
    {
        public AssetCell()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Expand;

            var assetPath = new Label
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalTextAlign = UITextAlign.Start
            };
            
            var importedSize = new Label
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var rawSize = new Label
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var percentage = new Label
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };

            assetPath.Bind(Label.TextProperty, "AssetPath", BindingMode.ReadOnly);
            importedSize.Bind(Label.TextProperty, "ImportedSize", BindingMode.ReadOnly);
            rawSize.Bind(Label.TextProperty, "RawSize", BindingMode.ReadOnly);
            percentage.Bind(Label.TextProperty, "Percentage", BindingMode.ReadOnly);
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = {
                    assetPath, 
                    importedSize,
                    rawSize,
                    percentage
                }
            };
        }
    }
}