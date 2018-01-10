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

            var assetPath = new LabelView
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalTextAlign = UITextAlign.Start
            };
            
            var importedSize = new LabelView
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var rawSize = new LabelView
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var percentage = new LabelView
            {
                Style = NoBackgroundNoOutline.Style,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };

            assetPath.Bind(LabelView.TextProperty, "AssetPath", BindingMode.ReadOnly);
            importedSize.Bind(LabelView.TextProperty, "ImportedSize", BindingMode.ReadOnly);
            rawSize.Bind(LabelView.TextProperty, "RawSize", BindingMode.ReadOnly);
            percentage.Bind(LabelView.TextProperty, "Percentage", BindingMode.ReadOnly);
            
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