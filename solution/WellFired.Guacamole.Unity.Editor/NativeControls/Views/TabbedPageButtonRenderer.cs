using System.ComponentModel;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;

[assembly: CustomRenderer(typeof(TabbedPageButtonView), typeof(TabbedPageButtonRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class TabbedPageButtonRenderer : ButtonRenderer
    {
        public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            var tabbedPageButton = (TabbedPageButtonView)Control;
            
            if (e.PropertyName == TabbedPageButtonView.IsSelectedProperty.PropertyName)
            {
                if (tabbedPageButton.IsSelected)
                {
                    
                }
            }
        }
    }
}