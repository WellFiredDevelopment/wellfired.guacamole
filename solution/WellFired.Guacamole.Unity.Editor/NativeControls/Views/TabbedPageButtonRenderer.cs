using System.ComponentModel;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(TabbedPageButton), typeof(TabbedPageButtonRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class TabbedPageButtonRenderer : ButtonRenderer
    {
        public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            var tabbedPageButton = (TabbedPageButton)Control;
            
            if (e.PropertyName == TabbedPageButton.IsSelectedProperty.PropertyName)
            {
                if (tabbedPageButton.IsSelected)
                {
                    
                }
            }
        }
    }
}