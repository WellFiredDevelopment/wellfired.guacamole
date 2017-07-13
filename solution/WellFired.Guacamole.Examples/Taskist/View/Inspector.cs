using WellFired.Guacamole.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.Taskist.View.Cells;
using WellFired.Guacamole.Examples.Taskist.ViewModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class Inspector : Views.View
    {
        public Inspector()
        {
            OutlineColor = UIColor.FromRGB(241, 241, 241);
            BackgroundColor = UIColor.FromRGB(250, 250, 250);
            MinSize = UISize.Of(300, 30);
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Expand;
            Padding = UIPadding.With(30, 0, 0, 0);

            var collection = new ObservableCollection<Filter>
            {
                new Filter { FilterName = "Filter 0" },
                new Filter { FilterName = "Filter 1" },
                new Filter { FilterName = "Filter 2" },
                new Filter { FilterName = "Filter 3" },
                new Filter { FilterName = "Filter 4" }
            };

            Content = new ListView
            {
                OutlineColor = UIColor.FromRGB(250, 250, 250),
                BackgroundColor = UIColor.FromRGB(250, 250, 250),
                Spacing = 8,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center,
                ItemTemplate = DataTemplate.Of(typeof(FilterCell)),
                ItemSource = collection
            };
        }
    }
}