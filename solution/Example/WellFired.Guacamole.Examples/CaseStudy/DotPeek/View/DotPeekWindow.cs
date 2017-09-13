using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.OverviewPage;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.ProjectSettingsPage;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.UsedAssetsPage;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.UIElements;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Assets;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Overview;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.ProjectSettings;
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
                new Dictionary<Type, Style>
                {
                    {typeof(Label), Styles.Label.Style},
                    {typeof(LayoutView), Styles.LayoutView.Style},
                    {typeof(ListView), Styles.ListView.Style},
                    {typeof(Cell), Styles.Cell.Style},
                    {typeof(ViewContainer), Styles.ViewContainer.Style},
                    {typeof(HeaderButton), Styles.HeaderButton.Style},
                    {typeof(PreprocessorCell), Styles.PreprocessorCell.Style}
                }
            );

            BackgroundColor = UIColor.FromRGB(40, 40, 40);
            
            var overviewVM = GetOverviewBindingContext();
            var usedAssetVM = GetUsedAssetsBindingContext();
            var unusedAssetVM = GetUnusedAssetsBindingContext();
            var projectSettingsVM = GetProjectSettingsBindingContext();

            var tabbedPage = new TabbedPage
            {
                ItemSource = new object[] {overviewVM, usedAssetVM, unusedAssetVM, projectSettingsVM},
                ItemTemplate = DataTemplate.Of(o =>
                {
                    Page page = null;
                    if (o == overviewVM)
                        page = new OverviewPage {Title = "Overview"};
                    else if (o == usedAssetVM)
                        page = new AssetsPage {Title = "Used Assets"};
                    else if (o == unusedAssetVM)
                        page = new AssetsPage {Title = "Unused Assets"};
                    else if (o == projectSettingsVM)
                        page = new ProjectSettingsPage {Title = "Project Settings"};

                    return page;
                })
            };

            SetContent(tabbedPage);
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

        private static AssetsVM GetUnusedAssetsBindingContext()
        {
            var newReport = ModelGenerator.GetCurrentReport();
            var previousReport = ModelGenerator.GetPreviousReport();

            var usedAssets = new List<IAsset>();
            usedAssets.AddRange(newReport.UnusedAssets);

            var previousUsedAssets = new List<IAsset>();
            previousUsedAssets.AddRange(previousReport.UnusedAssets);

            return new AssetsVM(usedAssets, previousUsedAssets);
        }

        private static ProjectSettingsVM GetProjectSettingsBindingContext()
        {
            var newReport = ModelGenerator.GetCurrentReport();
            var previousReport = ModelGenerator.GetPreviousReport();
            return new ProjectSettingsVM(newReport, previousReport);
        }
    }
}