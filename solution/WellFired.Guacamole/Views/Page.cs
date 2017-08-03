using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public class Page : View
    {
        [PublicAPI] public static readonly BindableProperty TitleProperty = BindableProperty.Create<Page, string>(
            "Page",
            BindingMode.TwoWay,
            page => page.Title
        );
        
        public string Title 
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        
        public Page()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Fill;
        }
    }
}