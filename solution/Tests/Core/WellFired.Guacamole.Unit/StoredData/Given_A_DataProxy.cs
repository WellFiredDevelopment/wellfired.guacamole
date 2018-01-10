using NUnit.Framework;
using WellFired.Guacamole.StoredData.Serialization;
using WellFired.Guacamole.StoredData.Synchronization;

namespace WellFired.Guacamole.Unit.StoredData
{
	[TestFixture]
	public class GivenADataProxy
	{
		[Test]
		//When we inject data in the proxy, we should updates its properties, but we should not consider the data it holds
		//as updated since he got data injected from outside.
		public void When_Data_Injected_In_Proxy_Then_DataProxy_Properties_Are_Synchronized_But_DataChanged_Set_To_False()
		{
			var optionProxy = new OptionsProxy();

			var options = new Options {OpenAutomatically = true, RelativePath = "a/relative/path"};
			var serializedOptionsFromStorage = new JSONSerializer().Serialize(options);
			
			optionProxy.InjectData(serializedOptionsFromStorage);
			
			//Data Proxy properties are synchronized
			Assert.IsTrue(optionProxy.OpenAutomatically);
			Assert.AreEqual("a/relative/path", optionProxy.RelativePath);
			
			//Data Proxy extracted data is also correct
			var serializedContent = optionProxy.GetData();
			var cachedOptions = new JSONSerializer().Unserialize<Options>(serializedContent);
			Assert.IsTrue(cachedOptions.OpenAutomatically);
			Assert.AreEqual("a/relative/path", cachedOptions.RelativePath);
			
			//Data is not considered as having changed since injected from storage
			Assert.IsFalse(optionProxy.DataChanged);
		}
		
		[Test]
		public void When_Data_Injected_In_Proxy_Cannot_Be_Parsed_Then_DataProxy_Instantiate_Default_Data_And_Ensure_Data_Changed_Is_Flagged()
		{
			var optionProxy = new OptionsProxy();
			optionProxy.InjectData(null);
			
			//Data Proxy properties are set to data default values 
			Assert.IsFalse(optionProxy.OpenAutomatically);
			Assert.AreEqual("default path", optionProxy.RelativePath);
			
			//Data Proxy extracted data is also correct
			var serializedContent = optionProxy.GetData();
			var cachedOptions = new JSONSerializer().Unserialize<Options>(serializedContent);
			Assert.IsFalse(cachedOptions.OpenAutomatically);
			Assert.AreEqual("default path", cachedOptions.RelativePath);
			
			//Data is considered as having changed since a new data object was instanciated.
			Assert.IsTrue(optionProxy.DataChanged);
		}
		
		[Test]
		public void When_DataProxy_Property_Change_Then_CachedData_Is_Synchronized_And_DataChanged_Set_To_True()
		{
			var optionProxy = new OptionsProxy();

			var options = new Options {OpenAutomatically = true, RelativePath = "a/relative/path"};
			var serializedOptionsFromStorage = new JSONSerializer().Serialize(options);
			
			optionProxy.InjectData(serializedOptionsFromStorage);
			
			//Data Proxy properties are synchronized
			Assert.IsTrue(optionProxy.OpenAutomatically);
			Assert.AreEqual("a/relative/path", optionProxy.RelativePath);

			optionProxy.OpenAutomatically = false;
			optionProxy.RelativePath = "a/new/relative/path";
			
			//Data Proxy extracted data is correct
			var serializedContent = optionProxy.GetData();
			var cachedOptions = new JSONSerializer().Unserialize<Options>(serializedContent);
			Assert.IsFalse(cachedOptions.OpenAutomatically);
			Assert.AreEqual("a/new/relative/path", cachedOptions.RelativePath);
			Assert.IsTrue(optionProxy.DataChanged);
		}
		
		private class Options
		{
			public bool OpenAutomatically;
			public string RelativePath = "default path";
		}

		private class OptionsProxy : DataProxy<Options>
		{
			private bool _openAutomatically;
			private string _relativePath; 

			public bool OpenAutomatically
			{
				get => _openAutomatically;
				set => SetProperty(ref _openAutomatically, value);
			}

			public string RelativePath
			{
				get => _relativePath;
				set => SetProperty(ref _relativePath, value);
			}
		}
	}
}