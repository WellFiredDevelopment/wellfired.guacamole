using System.ComponentModel;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(FilterView), typeof(FilterViewViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class FilterViewViewRenderer : TextEntryViewRenderer
	{
		public override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnViewPropertyChanged(sender, e);
			
			var control = (FilterView) Control;
			if (e.PropertyName == TextEntryView.TextProperty.PropertyName)
			{
				control.Search();
			}
		}
	}
}