using System.Collections.ObjectModel;
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

            var collection = new ObservableCollection<string> { "One", "Two", "Three", "Four", "Five", "Six", "Seven" };

            Content = new ListView {
                BackgroundColor = UIColor.Blue,
                HorizontalLayout = LayoutOptions.Center,
                VerticalLayout = LayoutOptions.Center,
                ItemSource = collection
            };
        }
    }
}