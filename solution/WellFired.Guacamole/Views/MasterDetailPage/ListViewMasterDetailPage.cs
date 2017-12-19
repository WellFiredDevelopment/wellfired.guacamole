﻿using System;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views.MasterDetailPage
{
	public class ListViewMasterDetailPage : MasterDetailPage
	{
		public ListViewMasterDetailPage(ListView master, IView detail) : base(master, detail)
		{
			master.OnItemSelected += OnItemSelected;
		}

		private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem is MasterPageItem item)
				SetDetail((Page)Activator.CreateInstance (item.TargetType));
		}
	}
}