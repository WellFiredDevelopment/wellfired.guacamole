using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TextFieldExample
{
	public class TextFieldTestWindow : Window
	{
		public TextFieldTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);
			Content = new LayoutView {
				Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
				Children = { new TextEntryView {PlaceholderText = "Type here..."} }
			};
		}
	}
}