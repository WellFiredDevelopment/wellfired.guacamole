﻿using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TwoWayBindingExample
{
	public class TwoWayBindingTestWindow : Window
	{
		public TwoWayBindingTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var boundTextEntry = new TextEntryView();

			Content = boundTextEntry;
			BindingContext = persistantData;

			boundTextEntry.Bind(TextEntryView.TextProperty, "BoundText", BindingMode.TwoWay);
		}
	}
}