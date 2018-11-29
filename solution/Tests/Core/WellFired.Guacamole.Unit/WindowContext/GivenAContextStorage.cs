using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.DataStorage.Data.Serialization;
using WellFired.Guacamole.DataStorage.Types;
using WellFired.Guacamole.WindowContext;

namespace WellFired.Guacamole.Unit.WindowContext
{
	[TestFixture]
	public class GivenAContextStorage
	{
		[Test]
		public void When_Instantiated_And_StoredContext_Data_Does_Not_Exist_Then_Create_It()
		{
			StoredContexts createdContext = null;
			
			var serializer = Substitute.For<ISerializer>();
			serializer.When(x => x.Serialize(Arg.Any<StoredContexts>())).Do(x => createdContext = (StoredContexts) x[0]);
			
			var storage = Substitute.For<IDataStorageService>();
			storage.Exists("StoredContexts").Returns(false);

			var unused = new ContextStorage(storage, serializer);
			
			Assert.IsNotNull(createdContext);
			Assert.IsNotNull(createdContext.ContextIds);
		}

		[Test]
		public void When_Save_Context_Then_It_Is_Also_Saved_In_StoredContext_List()
		{
			var context = new Context();
			
			var serializer = Substitute.For<ISerializer>();
			serializer.Serialize(context).Returns("serialized data");
			
			var storage = Substitute.For<IDataStorageService>();
			
			var storedContext = new StoredContexts();
			serializer.Unserialize<StoredContexts>("serialized stored context").Returns(storedContext);
			storage.Read("Contexts").Returns("serialized stored context");
			serializer.Serialize(storedContext).Returns("serialized updated stored context");
			
			var contextStorage = new ContextStorage(storage, serializer);
			
			storage.ClearReceivedCalls();
			serializer.ClearReceivedCalls();
			
			contextStorage.Save("oij32", context);
			
			Assert.That(() => storage.Received(1).Write("serialized data", "oij32"), Throws.Nothing);
			Assert.IsTrue(storedContext.ContextIds.Contains("oij32"));
			Assert.That(() => storage.Received(1).Write("serialized updated stored context", "Contexts"), Throws.Nothing);
		}

		[Test]
		public void When_Delete_Context_Then_Remove_It_From_StoredContext()
		{
			var storedContext = new StoredContexts();
			storedContext.ContextIds.Add("oij32");
			
			var serializer = Substitute.For<ISerializer>();
			serializer.Unserialize<StoredContexts>("serialized stored context").Returns(storedContext);
			serializer.Serialize(storedContext).Returns("serialized updated stored context");
			
			var storage = Substitute.For<IDataStorageService>();
			storage.Read("Contexts").Returns("serialized stored context");
			
			var contextStorage = new ContextStorage(storage, serializer);
			
			storage.ClearReceivedCalls();
			serializer.ClearReceivedCalls();
			
			contextStorage.Delete("oij32");
			
			Assert.That(() => storage.Received(1).Delete("oij32"), Throws.Nothing);
			Assert.IsTrue(storedContext.ContextIds.Count == 0);
			Assert.That(() => storage.Received(1).Write("serialized updated stored context", "Contexts"), Throws.Nothing);
		}
	}
}