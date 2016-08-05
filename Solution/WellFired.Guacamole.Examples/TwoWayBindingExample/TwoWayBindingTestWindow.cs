using System.ComponentModel;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.TwoWayBindingExample
{
    public class TwoWayBindingTestWindow : Window
    {
        public TwoWayBindingTestWindow(INotifyPropertyChanged persistantData)
        {
            Padding = new UIPadding(5);

            var boundTextEntry = new TextEntry ();

            Content = boundTextEntry;
            BindingContext = persistantData;

            boundTextEntry.Bind(TextEntry.TextProperty, "BoundText", BindingMode.TwoWay);
        }
    }
}