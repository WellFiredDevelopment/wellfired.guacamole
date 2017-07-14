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
            Padding = UIPadding.Of(5);
            Content = new ListView { ItemSource = ItemSource.From("One", "Two", "Three") };
        }
    }
}