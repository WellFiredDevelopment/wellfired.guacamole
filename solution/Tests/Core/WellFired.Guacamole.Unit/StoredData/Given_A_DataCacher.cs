using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.StoredData;
using WellFired.Guacamole.StoredData.Exceptions;

namespace WellFired.Guacamole.Unit.StoredData
{
	[TestFixture]
	public class Given_A_DataCacher
	{
		[Test]
		public void When_Cache_Twice_DataProxy_For_Same_Key_Exception_Is_Raised()
		{
			var dataProxy = Substitute.For<IDataProxy>();
			var dataCacher = new DataCacher();
			dataCacher.Cache("Options", dataProxy);
			Assert.That(() => dataCacher.Cache("Options", dataProxy), Throws.TypeOf<ProxyDataAlreadyAssignedException>());
		}
		
		[Test]
		public void When_Get_Data_At_Specific_Key_Then_Right_Data_Is_Returned()
		{
			var dataProxy = Substitute.For<IDataProxy>();
			dataProxy.GetData().Returns("serialized data");
			
			var dataCacher = new DataCacher();
			dataCacher.Cache("Options", dataProxy);
			
			Assert.That(dataCacher.GetData("Options"), Is.EqualTo("serialized data"));
		}
		
		[Test]
		public void When_Check_Cached_Data_Changed_Then_Right_Value_Is_Returned()
		{
			var dataProxy = Substitute.For<IDataProxy>();
			dataProxy.DataChanged.Returns(true);
			
			var dataCacher = new DataCacher();
			dataCacher.Cache("Options", dataProxy);

			Assert.IsTrue(dataCacher.DidDataChanged("Options"));
		}
		
		[Test]
		public void When_Update_Cached_Data_At_Specific_Key_Then_Right_DataProxy_Get_Injected()
		{
			var dataProxy = Substitute.For<IDataProxy>();
			
			var dataCacher = new DataCacher();
			dataCacher.Cache("Options", dataProxy);
			dataCacher.UpdateData("Options", "content injected");

			Assert.That(() => dataProxy.Received(1).InjectData("content injected"), Throws.Nothing);
		}
		
		[Test]
		public void When_Reset_Cached_Data_Changed_State_At_Specific_Key_Then_Right_DataProxy_Get_Reset()
		{
			var dataProxy = Substitute.For<IDataProxy>();
			
			var dataCacher = new DataCacher();
			dataCacher.Cache("Options", dataProxy);
			dataCacher.ResetDataChanged("Options");

			Assert.That(() => dataProxy.Received(1).ResetDataChanged(), Throws.Nothing);
		}
	}
}