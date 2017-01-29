using System.Collections.Generic;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.ListViewExample
{
    public class ListViewTestWindow : Window
    {
        public ListViewTestWindow()
        {
            Padding = UIPadding.Of(5);
            Content = new ListView { ItemSource = ItemSource.From("One", "Two", "Three") };
        }
    }
}