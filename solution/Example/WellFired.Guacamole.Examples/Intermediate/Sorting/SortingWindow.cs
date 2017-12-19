using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting
{
    public class SortingWindow : Window
    {
        public SortingWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
            : base(logger, persistantData, platformProvider)
        {
            Content = new BreakdownPage();
        }
    }
}