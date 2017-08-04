using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.OverviewPage;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.UsedAssetsPage;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.UIElements;
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
                    { typeof(LayoutView), Styles.LayoutView.Style},
                    { typeof(ListView), Styles.ListView.Style},
                    { typeof(Cell), Styles.Cell.Style},
                    { typeof(ViewContainer), Styles.ViewContainer.Style},
                    { typeof(ColumnLegendButton), Styles.ColumnLegendButton.Style}
                }
            );

//            var overviewPage = new OverviewPage();
//            SetContent(overviewPage);
//            overviewPage.BindingContext = GetOverviewBindingContext();
            
            var usedAssetsPage = new AssetsPage();
            SetContent(usedAssetsPage);
            usedAssetsPage.BindingContext = GetUsedAssetsBindingContext();
        }

        private static OverviewVM GetOverviewBindingContext()
        {
            var newReport = ModelGenerator.GetCurrentReport();
            var previousReport = ModelGenerator.GetPreviousReport();
            return new OverviewVM(newReport, previousReport);
        }

        private static AssetsVM GetUsedAssetsBindingContext()
        {
            var newReport = ModelGenerator.GetCurrentReport();
            var previousReport = ModelGenerator.GetPreviousReport();
            
            var usedAssets = new List<IAsset>();
            usedAssets.AddRange(newReport.NonResourcesIncludedAssets);
            usedAssets.AddRange(newReport.ResourcesIncludedAssets);
            
            var previousUsedAssets = new List<IAsset>();
            previousUsedAssets.AddRange(previousReport.NonResourcesIncludedAssets);
            previousUsedAssets.AddRange(previousReport.ResourcesIncludedAssets);
            
            return new AssetsVM(usedAssets, previousUsedAssets);
        }
    }
}