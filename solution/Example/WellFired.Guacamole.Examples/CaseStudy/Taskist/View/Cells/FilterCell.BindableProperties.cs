using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells
{
    public partial class FilterCell
    {
        [PublicAPI] 
        public static readonly BindableProperty FilterColorProperty = BindableProperty
            .Create<FilterCell, UIColor>(
                UIColor.FromRGB(236, 142, 117),
                BindingMode.TwoWay,
                v => v.FilterColor
            );

        [PublicAPI] 
        public static readonly BindableProperty FilterTextProperty = BindableProperty
            .Create<FilterCell, string>(
                string.Empty,
                BindingMode.TwoWay,
                v => v.Text
            );
        
        [PublicAPI]
        public UIColor FilterColor
        {
            get { return (UIColor) GetValue(FilterColorProperty); }
            set { SetValue(FilterColorProperty, value); }
        }
        
        [PublicAPI]
        public string Text
        {
            get { return (string) GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }
    }
}