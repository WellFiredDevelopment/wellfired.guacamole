using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.FilterExample
{
	public class FilterTestWindow : Window
	{
		public FilterTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) : base(logger, persistantData, platformProvider)
		{
			var introLabel = new LabelView {
				Text = "Filter is a Text Entry that process the input to extract a list of words, and" +
				       " key/value pairs entered by the user. For example, \"sausage, guacamole t:prefab count:3\", will return" +
				       " a list {sausage, guacamole} and a dictionary {{t, prefab}, {count, 3}}.",
				Padding = UIPadding.Of(5)
			};
			var filter = new FilterView{Padding = UIPadding.Of(5)};
			var label = new LabelView{Padding = UIPadding.Of(5)};
Padding = UIPadding.Of(30);
			Content = LayoutView.WithAdjacentVertical(new List<ILayoutable> {introLabel, filter, label});
			
			BindingContext = new ViewModel();
			
			filter.Bind(FilterView.SimpleSearchProperty, "SimpleSearch", BindingMode.TwoWay);
			filter.Bind(FilterView.KeyValueSearchProperty, "KeyValueSearch", BindingMode.TwoWay);
			label.Bind(LabelView.TextProperty, "Text", BindingMode.OneWay);
		}
	}
}