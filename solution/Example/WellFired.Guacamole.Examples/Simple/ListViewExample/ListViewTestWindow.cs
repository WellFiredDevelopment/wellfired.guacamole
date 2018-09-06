using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ListViewExample
{
    public class ListViewTestWindow : Window
    {
        public ListViewTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
            : base(logger, persistantData, platformProvider)
        {
            var listView = new ListView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                Orientation = OrientationOptions.Horizontal,
                EntrySize = 50,
                Spacing = 5,
                ItemSource = ItemSource.From("One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight"),
                CanMultiSelect = true,
                ShouldShowScrollBar = true
            };
            
            var label = new LabelView
            {
                Text = "You can select an element by clicking on it. You can also multi-select an element by clicking on" +
                       " Ctrl or Command"
            };
            
            Content = LayoutView.WithAdjacentVertical(new ILayoutable[]{label, listView});
        }
    }
}