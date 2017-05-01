using System.Collections.ObjectModel;
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
            OutlineColor = UIColor.Black;
            BackgroundColor = UIColor.Red;
            MinSize = UISize.Of(300, 30);
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;

            var collection = new ObservableCollection<Filter>
            {
                new Filter {FilterName = "Filter 0"},
                new Filter {FilterName = "Filter 1"}
            };

            Content = new ListView
            {
                BackgroundColor = UIColor.Blue,
                HorizontalLayout = LayoutOptions.Center,
                VerticalLayout = LayoutOptions.Center,
                ItemTemplate = DataTemplate.Of(typeof(FilterCell)),
                ItemSource = collection
            };
        }
    }
}