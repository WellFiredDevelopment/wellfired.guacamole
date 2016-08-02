namespace WellFired.Guacamole.Examples.UIBinding
{
    // ReSharper disable once InconsistentNaming
	public class UIBindingTestWindow : Window
	{
		public UIBindingTestWindow()
		{
			Padding = new UIPadding(5);
			
			var destinationElement = new TextEntry
			{
				BackgroundColor = UIColor.White,
				OutlineColor = UIColor.Black
			};

			var sourceElement = new Slider
			{
				BackgroundColor = UIColor.White,
				MinValue = 0,
				MaxValue = 32
			};

			Content = new AdjacentLayout
			{
				Children =
				{
					destinationElement,
					sourceElement
				}
			};

			destinationElement.BindingContext = sourceElement;

			destinationElement.Bind(CornerRadiusProperty, "Value");
		}
	}
}