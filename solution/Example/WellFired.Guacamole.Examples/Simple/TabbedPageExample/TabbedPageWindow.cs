using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.Simple.TabbedPageExample.ViewModel;
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
				ItemSource = new object [] { new FirstPage(), new SecondPage(), new ThirdPage() },
				ItemTemplate = DataTemplate.Of(o => {
					if (o is FirstPage)
						return new Page {Title = "First", BackgroundColor = UIColor.Aquamarine, OutlineColor = UIColor.Aquamarine};
					if (o is SecondPage)
						return new Page {Title = "Second", BackgroundColor = UIColor.Beige, OutlineColor = UIColor.Beige};
					return new Page {Title = "Third", BackgroundColor = UIColor.Brown, OutlineColor = UIColor.Brown};
				})
			};
		}
	}
}