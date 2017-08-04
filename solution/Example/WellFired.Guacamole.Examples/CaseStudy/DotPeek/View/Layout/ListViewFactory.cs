using System;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.UIElements;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Layout
{
    public static class ListViewFactory
    {
        public struct LegendDefinition
        {
            public string Title;
            public UISize MinSize;
            public UISize MaxSize;

            public LegendDefinition(string title, UISize minSize, UISize maxSize)
            {
                Title = title;
                MinSize = minSize;
                MaxSize = maxSize;
            }
        }
        
        public static Views.View CreateListView(Type template, string bindedProperty, params LegendDefinition[] legends)
        {
            // ReSharper disable once CoVariantArrayConversion
            ILayoutable[] legendButtons = legends.Select(GenerateLegendButton).ToArray();
            
            var columnLegendView = LayoutFactory.CreateHorizontalLayout(legendButtons);
            columnLegendView.Padding = UIPadding.With(0, 0, 0, 10);
            
            var list = new ListView {
                Spacing = 10,
                Orientation = OrientationOptions.Vertical,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                ItemTemplate = DataTemplate.Of(template),
            };
            
            list.Bind(ItemsView.ItemSourceProperty, bindedProperty);
            
            
            return LayoutFactory.CreateVerticalLayout(columnLegendView, list);
        }

        private static ColumnLegendButton GenerateLegendButton(LegendDefinition legend)
        {
            return new ColumnLegendButton
            {
                Text = legend.Title,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalLayout = LayoutOptions.Fill,
                BackgroundColor = UIColor.Black,
                MinSize = legend.MinSize,
                MaxSize = legend.MaxSize
            };
        }
    }
}