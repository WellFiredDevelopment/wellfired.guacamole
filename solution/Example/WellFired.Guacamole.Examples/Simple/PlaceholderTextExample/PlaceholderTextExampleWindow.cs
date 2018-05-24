using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.PlaceholderTextExample
{
	public class PlaceholderTextExampleWindow : Window
	{
		public PlaceholderTextExampleWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			var forceText1Button = new ButtonView { Text = "button 1" };
			var forceText2Button = new ButtonView { Text = "button 2" };
			
			var textEntry1View = new TextEntryView {PlaceholderText = "Type here..."};
			var textEntry2View = new TextEntryView {PlaceholderText = "Also Type here..."};
			
			forceText1Button.ButtonPressedCommand = new Command {
				CanExecute = true,
				ExecuteAction = () => { textEntry1View.Text = "Button 1 Was Pressed"; }
			};
			
			forceText2Button.ButtonPressedCommand = new Command {
				CanExecute = true,
				ExecuteAction = () => { textEntry2View.Text = "Button 2 Was Pressed"; }
			};
			
			Padding = UIPadding.Of(5);
			Content = new LayoutView {
				Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
				Children = {
					textEntry1View, 
					textEntry2View,
					forceText1Button,
					forceText2Button
				}
			};
		}
	}
}