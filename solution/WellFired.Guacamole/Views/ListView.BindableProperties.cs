using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class ListView
    {
        [PublicAPI] public static readonly BindableProperty SpacingProperty = BindableProperty.Create<ListView, int>(
            default(int),
            BindingMode.TwoWay,
            listView => listView.Spacing
        );

        [PublicAPI]
        public int Spacing
        {
            get { return (int) GetValue(SpacingProperty); }
            set
            {
                if (!SetValue(SpacingProperty, value)) 
                    return;
                
                var adjacentLayout = Layout as AdjacentLayout;
                if (adjacentLayout != null) 
                    adjacentLayout.Spacing = Spacing;
            }
        }
    }
}