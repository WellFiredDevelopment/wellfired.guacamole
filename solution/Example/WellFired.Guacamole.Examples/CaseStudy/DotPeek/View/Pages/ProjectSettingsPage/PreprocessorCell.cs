using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.ProjectSettingsPage
{
    public class PreprocessorCell : Cell
    {
        public PreprocessorCell()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Expand;
            BackgroundColor = UIColor.Black;

            var preprocessor = new Label
            {
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Aquamarine,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
            };

            Content = preprocessor;
            
            preprocessor.Bind(Label.TextProperty, "Preprocessor", BindingMode.ReadOnly);
            preprocessor.Bind(BackgroundColorProperty, "PreprocessorBackgroundColor", BindingMode.ReadOnly);
        }
    }
}