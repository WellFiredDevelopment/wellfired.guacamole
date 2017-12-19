using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
	[TestFixture]
	public class WithOneTierDataNoObservables
	{
		private Data.CompositeCollection _compositeCollection;
		private List<GroupEntry> _rawItemSource;

		[SetUp]
		public void SetUp()
		{
			_rawItemSource = new List<GroupEntry> {
				new GroupEntry("Amelia"),
				new GroupEntry("Alfie"),
				new GroupEntry("Archie"),
				new GroupEntry("Brooke"),
				new GroupEntry("Bobby"),
				new GroupEntry("Bella"),
				new GroupEntry("Ben"),
				new GroupEntry("Bump"),
				new GroupEntry("Calvin"),
				new GroupEntry("Calum"),
				new GroupEntry("Collin"),
				new GroupEntry("Cornelius"),
				new GroupEntry("Darren"),
				new GroupEntry("David"),
				new GroupEntry("Dennis"),
				new GroupEntry("Elvis"),
				new GroupEntry("Evelyn")
			};

			_compositeCollection = new Data.CompositeCollection(_rawItemSource);
		}

		[Test]
		public void When_Cleared_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			_rawItemSource.Clear();
			Assert.That(_compositeCollection.Count, Is.EqualTo(17));
			
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
		}

		[Test]
		public void When_TheCollectionIsModified_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			// Test child insert
			var newEntry = new GroupEntry("Some Test");
			_rawItemSource.Insert(3, newEntry);
			Assert.That(_compositeCollection.Count, Is.EqualTo(17));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test Child Add
			_rawItemSource.Add(newEntry);
			Assert.That(_compositeCollection.Count, Is.EqualTo(17));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test child Remove
			_rawItemSource.RemoveAt(1);
			Assert.That(_compositeCollection.Count, Is.EqualTo(17));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test child Replace (Replace Bobby with Collin)
			_rawItemSource[4] = _rawItemSource[9];
			Assert.That(_compositeCollection.Count, Is.EqualTo(17));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
		}
	}
}