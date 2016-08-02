namespace WellFired.Guacamole.Examples.TextField
{
	public class TextFieldTestWindow : Window
	{
		public TextFieldTestWindow()
		{
			BackgroundColor = UIColor.White;
			Padding = new UIPadding(5);

			Content = new TextEntry
			{
				VerticalTextAlign = UITextAlign.Middle,
				BackgroundColor = UIColor.Beige,
				Text = "Test"
			};
		}
	}
}