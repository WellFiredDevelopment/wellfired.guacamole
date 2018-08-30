using System.ComponentModel;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;

[assembly: CustomRenderer(typeof(TabbedPageButtonView), typeof(TabbedPageButtonViewRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class TabbedPageButtonViewRenderer : ButtonViewRenderer
    {
        public override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnViewPropertyChanged(sender, e);

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