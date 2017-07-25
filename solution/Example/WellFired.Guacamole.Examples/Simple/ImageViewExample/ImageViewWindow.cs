using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ImageViewExample
{
	public class ImageViewWindow : Window
	{
		public ImageViewWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);

			var imageView = new ImageView {
				BackgroundColor = UIColor.Black,
				ImageSource = ImageSource.From("doge.jpg")
			};

			Content = imageView;
		}
	}
}