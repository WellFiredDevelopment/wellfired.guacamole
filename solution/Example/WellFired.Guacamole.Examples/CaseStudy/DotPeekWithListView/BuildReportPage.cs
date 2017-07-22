using WellFired.Guacamole.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView.Cells;
using WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView.ViewModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView
{
    public class BuildReportPage : Page
    {
        public BuildReportPage()
        {
            var report0 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 98 };
            var report1 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 100 };
            var report2 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 99 };
            
            var collection = new ObservableCollection<BuildReport>
            {
                new BuildReport(report0, report1),
                new BuildReport(report1, report2),
                new BuildReport(report2, default(Model.BuildReport)),
            };

            Padding = 0;
            BackgroundColor = UIColor.White;
            Content = new ListView
            {
                EntrySize = 300,
                Spacing = 0,
                Orientation = OrientationOptions.Horizontal,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                ItemTemplate = DataTemplate.Of(typeof(BuildReportCell)),
                ItemSource = collection
            };
        }
    }
}