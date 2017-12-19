using System.ComponentModel;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(FilterView), typeof(FilterViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class FilterViewRenderer : TextEntryRenderer
	{
		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);
			
			var control = (FilterView) Control;
			if (e.PropertyName == TextEntry.TextProperty.PropertyName)
			{
				control.Search();
			}
		}
	}
}