using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View
{
    public class Overview : Views.View
    {
        public Overview()
        {
            BackgroundColor = UIColor.White;
            OutlineColor = UIColor.White;
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Fill;
            
            var collection = new ObservableCollection<Task>
            {
                new Task { Description = "Do The Thing" },
                new Task { Description = "Do The Other Thing" }
            };

            Content = new LayoutView
            {
                Padding = UIPadding.With(40, 60, 40, 0),
                OutlineColor = UIColor.Clear,
                BackgroundColor = UIColor.Clear,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical, 5),
                Children = {
                    new ListView
                    {
                        EntrySize = 38,
                        OutlineColor = UIColor.Clear,
                        BackgroundColor = UIColor.Clear,
                        HorizontalLayout = LayoutOptions.Fill,
                        VerticalLayout = LayoutOptions.Expand,
                        ItemTemplate = DataTemplate.Of(typeof(TaskCell)),
                        ItemSource = collection
                    },
                    new ButtonView
                    {
                        Text = "Add Task"
                    }
                }
            };
        }
    }
}