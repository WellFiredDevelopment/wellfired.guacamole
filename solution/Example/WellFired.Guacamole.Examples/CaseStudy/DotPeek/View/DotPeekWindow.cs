using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View
{
    public class DotPeekWindow : Window
    {
        public DotPeekWindow(ILogger logger, INotifyPropertyChanged persistantData) : base(logger, persistantData)
        {
            SetContent(new DotPeekMainPage());
            BindingContext = persistantData;
        }
    }
}