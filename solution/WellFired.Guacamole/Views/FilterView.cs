using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class FilterView : TextEntry
	{
		[PublicAPI] public static readonly BindableProperty KeyValueSearchProperty = BindableProperty.Create<FilterView, Dictionary<string, string>>(
			default(Dictionary<string, string>),
			BindingMode.TwoWay,
			v => v.KeyValueSearch
		);
		
		[PublicAPI] public static readonly BindableProperty SimpleSearchProperty = BindableProperty.Create<FilterView, List<string>>(
			default(List<string>),
			BindingMode.TwoWay,
			v => v.SimpleSearch
		);
		
		[PublicAPI]
		public Dictionary<string, string> KeyValueSearch
		{
			get => (Dictionary<string, string>) GetValue(KeyValueSearchProperty);
			set => SetValue(KeyValueSearchProperty, value);
		}
		
		[PublicAPI]
		public List<string> SimpleSearch
		{
			get => (List<string>) GetValue(SimpleSearchProperty);
			set => SetValue(SimpleSearchProperty, value);
		}

		public void Search()
		{
			KeyValueSearch = Text
				.Split(new[]{' ', ','}, StringSplitOptions.RemoveEmptyEntries)
				.Select(search => search.Split(new[]{':'}, StringSplitOptions.RemoveEmptyEntries))
				.Where(search => search.Length == 2).
				ToDictionary(keyValueSearch => keyValueSearch[0], keyValueEntry => keyValueEntry[1]);

			SimpleSearch = Text
				.Split(new[]{' ', ','}, StringSplitOptions.RemoveEmptyEntries)
				.Where(search => search.Split(':').Length == 1)
				.ToList();
		}
	}
}