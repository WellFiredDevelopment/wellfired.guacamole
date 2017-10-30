﻿using System.Collections;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ListViewWithBoundDataExample
{
	public class BoundObject : ObservableBase
	{
		public IList Data => ItemSource.From("First Sausage", "Second Sausage", "Third Sausage");
	}
	
	public class ListViewWithBoundDataTestWindow : Window
	{
		private readonly ListView _listView;

		public ListViewWithBoundDataTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			_listView = new ListView
			{
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Orientation = OrientationOptions.Vertical,
				EntrySize = 50,
				Spacing = 5,
				ItemSource = ItemSource.From("Sausage")
			};

			Content = _listView;

			_listView.BindingContext = new BoundObject();
			_listView.Bind(ItemsView.ItemSourceProperty, "Data", BindingMode.ReadOnly);
		}
	}
}