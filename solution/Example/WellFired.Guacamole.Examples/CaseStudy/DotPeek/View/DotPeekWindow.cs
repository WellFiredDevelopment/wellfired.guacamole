using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View
{
    public class DotPeekWindow : Window
    {
        public DotPeekWindow(ILogger logger, INotifyPropertyChanged persistantData) 
            : base(logger, persistantData)
        {   
            StyleDictionary = new StyleDictionary(
                logger,
                new Dictionary<Type, Style> {
                    { typeof(Label), Styles.Label.Style},
                    { typeof(LayoutView), Styles.LayoutView.Style}
                }
            );

            var overviewPage = new OverviewPage();
            SetContent(overviewPage);
            overviewPage.BindingContext = GetOverviewBindingContext();
        }

        private static OverviewVM GetOverviewBindingContext()
        {
            var newReport = ModelGenerator.GetCurrentReport();
            var previousReport = ModelGenerator.GetPreviousReport();
            return new OverviewVM(newReport, previousReport);
        }
    }
}