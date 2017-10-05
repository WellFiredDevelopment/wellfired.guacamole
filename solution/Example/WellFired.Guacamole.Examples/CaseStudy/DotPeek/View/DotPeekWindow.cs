using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View
{
    public class DotPeekWindow : Window
    {
        public DotPeekWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) : base(logger, persistantData, platformProvider)
        {
            SetContent(new DotPeekMainPage());
            BindingContext = persistantData;
        }
    }
}