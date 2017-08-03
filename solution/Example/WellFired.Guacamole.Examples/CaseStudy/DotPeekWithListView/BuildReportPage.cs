using WellFired.Guacamole.Collection;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView.Cells;
using WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView.ViewModel;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView
{
    public class BuildReportPage : Page
    {
        public BuildReportPage()
        {
            Title = "Report";
            var report0 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 98 };
            var report1 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 100 };
            var report2 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 99 };
            
            var report3 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 110 };
            var report4 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 50 };
            var report5 = new Model.BuildReport { BuildTime = 1000, Platform = "Unity Macos", UnityVersion = "2017.1", BuildSize = 49 };
            
            var collection = new ObservableCollection<BuildReport>
            {
                new BuildReport(report0, report1),
                new BuildReport(report1, report2),
                new BuildReport(report2, report3),
            };

            Padding = 0;
            BackgroundColor = UIColor.White;
            Content = new ListView
            {
                EntrySize = 290,
                Spacing = 20,
                Orientation = OrientationOptions.Horizontal,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                ItemTemplate = DataTemplate.Of(typeof(BuildReportCell)),
                ItemSource = collection
            };

            // 4. Here we add items to the collection after the view is created and the view is automatically updated.
            // The reason this is cool is because usually you wouldn't hard code the collection like this, usually
            // the collection would be part of the page VM, and the VM would deal with constructing these views 
            // (probably in an async manor), but the view would be automatically bound to the collection, so the user
            // has to do nothing other than .Add(new VM)
            collection.Add(new BuildReport(report3, report4));
            collection.Add(new BuildReport(report4, report5));
            collection.Add(new BuildReport(report5, default(Model.BuildReport)));
        }
    }
}