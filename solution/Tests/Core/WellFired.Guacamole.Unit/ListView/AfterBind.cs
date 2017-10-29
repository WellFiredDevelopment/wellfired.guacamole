using System.Collections;
using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.ListView
{
	[TestFixture]
	public class AfterBind
	{
		private Views.ListView _listView;

		[Test]
		public void AfterBindingToItemSource_ThenChildrenShouldBeCorrect()
		{
			_listView = new Views.ListView
			{
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Orientation = OrientationOptions.Vertical,
				EntrySize = 50,
				Spacing = 5,
				ItemSource = ItemSource.From("Sausage")
			};

			_listView.BindingContext = new BoundObject();
			_listView.Bind(ItemsView.ItemSourceProperty, "Data", BindingMode.ReadOnly);
			
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));

			Assert.That(_listView.ItemSource.Count, Is.EqualTo(3));
			Assert.That(_listView.Children.Count, Is.EqualTo(3));
		}
	}

	public class BoundObject : ObservableBase
	{
		public IList Data => ItemSource.From("First Sausage", "Second Sausage", "Third Sausage");
	}
}