using WellFired.Guacamole.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View
{
    public class Inspector : Views.View
    {
        public Inspector()
        {
            OutlineColor = UIColor.FromRGB(241, 241, 241);
            BackgroundColor = UIColor.FromRGB(250, 250, 250);
            MinSize = UISize.Of(300, 30);
            HorizontalLayout = LayoutOptions.Expand;
            VerticalLayout = LayoutOptions.Fill;
            Padding = UIPadding.With(30, 60, 10, 60);

            var collection = new ObservableCollection<Filter>();

            for (var n = 0; n < 10000; n++)
                collection.Add(new Filter { FilterName = $"Item {n}", FilterColor = UIColor.FromRGB(236, 142, 117) });

            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
                Children = {
                    new ListView {
                        EntrySize = 38,
                        OutlineColor = UIColor.FromRGB(250, 250, 250),
                        BackgroundColor = UIColor.FromRGB(250, 250, 250),
                        HorizontalLayout = LayoutOptions.Fill,
                        VerticalLayout = LayoutOptions.Expand,
                        ItemTemplate = DataTemplate.Of(typeof(FilterCell)),
                        ItemSource = collection
                    },
                    new Button
                    {
                        Text = "New",
                        HorizontalLayout = LayoutOptions.Expand
                    }
                }
            };
        }
    }
}