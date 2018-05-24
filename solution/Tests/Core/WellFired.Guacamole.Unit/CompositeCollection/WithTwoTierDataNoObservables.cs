using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
	[TestFixture]
	public class WithTwoTierDataNoObservables
	{
		private Data.CompositeCollection _compositeCollection;
		private List<Group> _rawItemSource;

		[SetUp]
		public void SetUp()
		{
			_rawItemSource = new List<Group> {
				new Group("A") {
					new GroupEntry("Amelia"),
					new GroupEntry("Alfie"),
					new GroupEntry("Archie")
				},
				new Group("B") {
					new GroupEntry("Brooke"),
					new GroupEntry("Bobby"),
					new GroupEntry("Bella"),
					new GroupEntry("Ben"),
					new GroupEntry("Bump")
				},
				new Group("C") {
					new GroupEntry("Calvin"),
					new GroupEntry("Calum"),
					new GroupEntry("Collin"),
					new GroupEntry("Cornelius")
				},
				new Group("D") {
					new GroupEntry("Darren"),
					new GroupEntry("David"),
					new GroupEntry("Dennis"),
				},
				new Group("E") {
					new GroupEntry("Elvis"),
					new GroupEntry("Evelyn")
				}
			};

			_compositeCollection = new Data.CompositeCollection(_rawItemSource);
		}

		[Test]
		public void When_NewGroupAdded_Then_ModificationIsNotPropogated()
		{		
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Receive;
			
			var group = new Group("F") {
				new GroupEntry("Fanny"),
				new GroupEntry("Farrah")
			};
			
			_rawItemSource.Add(group);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
		}

		[Test]
		public void When_GroupRemoved_Then_ModificationIsNotPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Receive;
			
			_rawItemSource.Remove(_rawItemSource[2]);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
		}

		[Test]
		public void When_GroupInserted_Then_ModificationIsNotPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Receive;
			
			var group = new Group("F") {
				new GroupEntry("Fanny"),
				new GroupEntry("Farrah")
			};
			_rawItemSource.Insert(2, group);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
		}

		[Test]
		public void When_GroupReplaced_Then_ModificationIsNotPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Receive;
			
			var group = new Group("F") {
				new GroupEntry("Fanny"),
				new GroupEntry("Farrah")
			};
			
			_rawItemSource[2] = group;
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
		}

		[Test]
		public void When_Cleared_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Receive;
			
			_rawItemSource.Clear();
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
		}

		[Test]
		public void When_TheCollectionIsModified_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Receive;
			
			// Test child insert
			var newEntry = new GroupEntry("Some Test");
			_rawItemSource[0].Insert(3, newEntry);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test Child Add
			_rawItemSource[4].Add(newEntry);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test child Remove
			_rawItemSource[0].RemoveAt(1);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test child Replace (Replace Bobby with Collin)
			_rawItemSource[1][1] = _rawItemSource[2][2];
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Receive(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
		}
	}
}