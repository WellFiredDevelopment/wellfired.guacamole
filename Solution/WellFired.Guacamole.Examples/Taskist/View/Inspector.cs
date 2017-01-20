using System.Collections.ObjectModel;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Taskist.View
{
    public class Inspector : Views.View
    {
        public Inspector()
        {
            OutlineColor = UIColor.Black;
            MinSize = new UISize(300, 0);
            VerticalLayout = LayoutOptions.Fill;
            BackgroundColor = UIColor.FromRGB(250, 250, 250);

            var collection = new ObservableCollection<string> { "One", "Two", "Three", "Four", "Five", "Six", "Seven" };

            Content = new List {
                ItemSource = collection
            };
        }
    }
}