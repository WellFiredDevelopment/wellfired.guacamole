using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.UIElements;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.ProjectSettings;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.ProjectSettingsPage
{
    public class ProjectSettingsPage : Page
    {
        public ProjectSettingsPage()
        {
            var preprocessorLabel = new Label
            {
                Text = "Preprocessors :",
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Expand,
                HorizontalLayout = LayoutOptions.Fill
            };
            
            var preprocessorList = new ListView {
                EntrySize = 14,
                Spacing = 7,
                Orientation = OrientationOptions.Vertical,
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                ItemTemplate = DataTemplate.Of(typeof(PreprocessorCell)),
            };
            
            preprocessorList.Bind(ItemsView.ItemSourceProperty, "PreprocessorsList", BindingMode.ReadOnly);

            var layout = LayoutFactory.CreateHorizontalLayout(preprocessorLabel, preprocessorList);
            layout.Padding = UIPadding.With(20, 50, 20, 0);
            
            Content = layout;

            BackgroundColor = UIColor.FromRGB(40, 40, 40);
        }
    }
}