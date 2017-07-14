using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek
{
    public class DotPeekWindow : Window
    {
        public DotPeekWindow(ILogger logger, INotifyPropertyChanged persistantData) 
            : base(logger, persistantData)
        {
            Content = new DotPeekPage();
        }
    }
}