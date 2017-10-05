using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ImageViewExample
{
	public class ImageViewWindow : Window
	{
		public ImageViewWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider)
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var imageView = new ImageView
			{	
				// Display an image from the hard drive.
				ImageSource = ImageSource.From("doge.jpg")
				
				// Display a programmatic image
				//ImageSource = ImageSource.From(ImageShape.Circle, UIColor.Blue, UIColor.Black)
				
				// Display an image from a URL
				//ImageSource = ImageSource.From(new Uri("https://upload.wikimedia.org/wikipedia/commons/7/75/La-Flotte-big-size-1-orignal.jpg"))
			};

			Content = imageView;
		}
	}
}