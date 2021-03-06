﻿using System;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.Simple.TabbedPageExample.ViewModel;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TabbedPageExample
{
	public class TabbedPageWindow : Window
	{
		public TabbedPageWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Content = new TabbedPage
			{
				ItemSource = new object [] { new FirstPage(), new SecondPage(), new ThirdPage() },
				ItemTemplate = DataTemplate.Of(o => {
					switch (o)
					{
						case FirstPage _:
							return new Page {
								Title = "First", 
								BackgroundColor = UIColor.Aquamarine, 
								OutlineThickness = 0, 
								Content = PageContent.Create()
							};
						case SecondPage _:
							return new Page {
								Title = "Second", 
								BackgroundColor = UIColor.Beige, 
								OutlineThickness = 0, 
								Content = PageContent.Create()
							};
						case ThirdPage _:
							return new Page {
								Title = "Third",
								BackgroundColor = UIColor.Brown,
								OutlineThickness = 0,
								Content = PageContent.Create()
							};
					}
					throw new NotImplementedException();
				})
			};
		}
	}
}