using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting
{
    public class SortingWindow : Window
    {
        public SortingWindow(ILogger logger, INotifyPropertyChanged persistantData) 
            : base(logger, persistantData)
        {
            Content = new BreakdownPage();
        }
    }
}