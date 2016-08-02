namespace WellFired.Guacamole.Examples.TextField
{
	public class ButtonTestWindow : Window
	{
		public ButtonTestWindow()
		{
			Padding = new UIPadding(5);

			Content = new Button
			{
				BackgroundColor = UIColor.White,
				Text = "Press Me Please."
			};
		}
	}
}