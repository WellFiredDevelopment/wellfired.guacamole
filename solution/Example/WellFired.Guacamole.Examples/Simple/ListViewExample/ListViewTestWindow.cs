using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ListViewExample
{
    public class ListViewTestWindow : Window
    {
        public ListViewTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
            : base(logger, persistantData)
        {
            Content = new ListView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                Orientation = OrientationOptions.Horizontal,
                EntrySize = 50,
                Spacing = 5,
                ItemSource = ItemSource.From("One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight")
            };
        }
    }
}