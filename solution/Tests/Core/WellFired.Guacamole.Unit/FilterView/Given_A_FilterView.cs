using System.Collections.Generic;
using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.FilterView
{
	[TestFixture]
	public class GivenAFilterView
	{
		[Test]
		public void When_Text_Input_Processed_Then_Search_Result_Is_Correct()
		{
			var filterView = new Views.FilterView();

			filterView.Text = "car character t:prefab";
			filterView.Search();
			Assert.That(filterView.SimpleSearch, Is.EquivalentTo(new []{"car", "character"}));
			Assert.That(filterView.KeyValueSearch, Is.EquivalentTo(new Dictionary<string, string>{{"t", "prefab"}}));

			filterView.Text = "sausages:guacamole:piccola, texture:car Assets/Prefab/";
			filterView.Search();
			Assert.That(filterView.SimpleSearch, Is.EquivalentTo(new []{"Assets/Prefab/"}));
			Assert.That(filterView.KeyValueSearch, Is.EquivalentTo(new Dictionary<string, string>{{"texture", "car"}}));
			
			filterView.Text = "piccola,texture:car, Assets/Prefab/ type:";
			filterView.Search();
			Assert.That(filterView.SimpleSearch, Is.EquivalentTo(new []{"piccola","Assets/Prefab/"}));
			Assert.That(filterView.KeyValueSearch, Is.EquivalentTo(new Dictionary<string, string>{{"texture", "car"}}));
			
			filterView.Text = "piccola,texture:car, Assets/Prefab/ type:martini type:cola";
			filterView.Search();
			Assert.That(filterView.SimpleSearch, Is.EquivalentTo(new[] {"piccola", "Assets/Prefab/"}));
			Assert.That(filterView.KeyValueSearch, Is.EquivalentTo(new Dictionary<string, string>{{"texture", "car"}, {"type","martini"}}));
		}

		[Test]
		public void Test_Binding()
		{
			var filterView = new Views.FilterView();
			var contextObject = new ContextObject();
			
			filterView.BindingContext = contextObject;
			filterView.Bind(Views.FilterView.SimpleSearchProperty, "SimpleSearch", BindingMode.TwoWay);
			filterView.Bind(Views.FilterView.KeyValueSearchProperty, "KeyValueSearch", BindingMode.TwoWay);
			
			filterView.KeyValueSearch = new Dictionary<string, string>{{"tomato", "hot"}, {"hotpot", "chengdu"}};
			filterView.SimpleSearch = new List<string>{"happy", "greedy"};
			
			Assert.That(contextObject.SimpleSearch, Is.EquivalentTo(filterView.SimpleSearch));
			Assert.That(contextObject.KeyValueSearch, Is.EquivalentTo(filterView.KeyValueSearch));
		}
		
		private class ContextObject : ObservableBase
		{
			private List<string> _simpleSearch;
			private Dictionary<string, string> _keyValueSearch;

			public List<string> SimpleSearch
			{
				get => _simpleSearch;
				// ReSharper disable once UnusedMember.Local
				set => SetProperty(ref _simpleSearch, value);
			}
			
			public Dictionary<string, string> KeyValueSearch
			{
				get => _keyValueSearch;
				// ReSharper disable once UnusedMember.Local
				set => SetProperty(ref _keyValueSearch, value);
			}
		}
	}
}