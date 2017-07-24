using System.ComponentModel;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.Simple.TabbedPageExample.ViewModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TabbedPageExample
{
	public class TabbedPageWindow : Window
	{
		public TabbedPageWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Content = new TabbedPage
			{
				ItemSource = new object [] { new FirstPage(), new SecondPage() },
				ItemTemplate = DataTemplate.Of(o => {
					if (o is FirstPage)
						return new Page {Title = "First", BackgroundColor = UIColor.Aquamarine};
					return new Page {Title = "Second", BackgroundColor = UIColor.Beige};
				})
			};
		}
	}
}