using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Examples.Simple.FilterExample
{
	public class ViewModel : NotifyBase
	{
		private List<string> _simpleSearch;
		private Dictionary<string, string> _keyValueSearch;
		private string _text;

		public List<string> SimpleSearch
		{
			get => _simpleSearch;
			set
			{
				SetProperty(ref _simpleSearch, value);
				UpdateText();
			}
		}

		public Dictionary<string, string> KeyValueSearch
		{
			get => _keyValueSearch;
			set
			{
				SetProperty(ref _keyValueSearch, value);
				UpdateText();
			}
		}

		public string Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		private void UpdateText()
		{
			var keyValueSearches = _keyValueSearch != null
				? string.Concat(_keyValueSearch.Select(keyValuePair => $"{{{keyValuePair.Key} : {keyValuePair.Value}}} ").ToArray())
				: "";

			var simpleSearch = _simpleSearch != null
				? string.Concat(_simpleSearch.Select(search => $"{search} ").ToArray())
				: "";
			
			Text = $"Key/Value searches : {keyValueSearches}\nSimple Search : {simpleSearch}";
		}
	}
}