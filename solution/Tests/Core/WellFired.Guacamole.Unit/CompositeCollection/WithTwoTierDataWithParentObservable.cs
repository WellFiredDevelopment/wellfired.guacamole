﻿using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
	[TestFixture]
	public class WithTwoTierDataWithParentObservable
	{
		private Data.CompositeCollection _compositeCollection;
		private ObservableCollection<Group> _rawItemSource;

		[SetUp]
		public void SetUp()
		{
			_rawItemSource = new ObservableCollection<Group> {
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
		public void TestingArrayAccess()
		{
			Assert.That(((IDefaultCellContext) _compositeCollection[0]).CellLabelText, Is.EqualTo("A"));
			Assert.That(((IDefaultCellContext) _compositeCollection[1]).CellLabelText, Is.EqualTo("Amelia"));
			Assert.That(((IDefaultCellContext) _compositeCollection[3]).CellLabelText, Is.EqualTo("Archie"));
			Assert.That(((IDefaultCellContext) _compositeCollection[4]).CellLabelText, Is.EqualTo("B"));
			Assert.That(((IDefaultCellContext) _compositeCollection[5]).CellLabelText, Is.EqualTo("Brooke"));
			Assert.That(((IDefaultCellContext) _compositeCollection[8]).CellLabelText, Is.EqualTo("Ben"));
			Assert.That(((IDefaultCellContext) _compositeCollection[9]).CellLabelText, Is.EqualTo("Bump"));
			Assert.That(((IDefaultCellContext) _compositeCollection[10]).CellLabelText, Is.EqualTo("C"));
			Assert.That(((IDefaultCellContext) _compositeCollection[11]).CellLabelText, Is.EqualTo("Calvin"));
			Assert.That(((IDefaultCellContext) _compositeCollection[14]).CellLabelText, Is.EqualTo("Cornelius"));
			Assert.That(((IDefaultCellContext) _compositeCollection[15]).CellLabelText, Is.EqualTo("D"));
		}

		[Test]
		public void When_NewGroupAdded_Then_ModificationIsPropogated()
		{		
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			var group = new Group("F") {
				new GroupEntry("Fanny"),
				new GroupEntry("Farrah")
			};
			
			_rawItemSource.Add(group);
			Assert.That(_compositeCollection.Count, Is.EqualTo(25));
			Assert.That(((IDefaultCellContext) _compositeCollection[22]).CellLabelText, Is.EqualTo("F"));
			Assert.That(((IDefaultCellContext) _compositeCollection[23]).CellLabelText, Is.EqualTo("Fanny"));
			Assert.That(((IDefaultCellContext) _compositeCollection[24]).CellLabelText, Is.EqualTo("Farrah"));
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewStartingIndex == 22));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewItems.Count == 3));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[0]).CellLabelText == "F"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[1]).CellLabelText == "Fanny"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[2]).CellLabelText == "Farrah"));
		}

		[Test]
		public void When_GroupRemoved_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			_rawItemSource.Remove(_rawItemSource[2]);
			Assert.That(_compositeCollection.Count, Is.EqualTo(17));
			Assert.That(((IDefaultCellContext) _compositeCollection[8]).CellLabelText, Is.EqualTo("Ben"));
			Assert.That(((IDefaultCellContext) _compositeCollection[9]).CellLabelText, Is.EqualTo("Bump"));
			Assert.That(((IDefaultCellContext) _compositeCollection[10]).CellLabelText, Is.EqualTo("D"));
			Assert.That(((IDefaultCellContext) _compositeCollection[11]).CellLabelText, Is.EqualTo("Darren"));
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.OldStartingIndex == 10));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.OldItems.Count == 5));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.OldItems[0]).CellLabelText == "C"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.OldItems[1]).CellLabelText == "Calvin"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.OldItems[2]).CellLabelText == "Calum"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.OldItems[3]).CellLabelText == "Collin"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.OldItems[4]).CellLabelText == "Cornelius"));
		}

		[Test]
		public void When_GroupInserted_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			var group = new Group("F") {
				new GroupEntry("Fanny"),
				new GroupEntry("Farrah")
			};
			_rawItemSource.Insert(2, group);
			
			Assert.That(_compositeCollection.Count, Is.EqualTo(25));
			Assert.That(((IDefaultCellContext) _compositeCollection[8]).CellLabelText, Is.EqualTo("Ben"));
			Assert.That(((IDefaultCellContext) _compositeCollection[9]).CellLabelText, Is.EqualTo("Bump"));
			Assert.That(((IDefaultCellContext) _compositeCollection[10]).CellLabelText, Is.EqualTo("F"));
			Assert.That(((IDefaultCellContext) _compositeCollection[11]).CellLabelText, Is.EqualTo("Fanny"));
			Assert.That(((IDefaultCellContext) _compositeCollection[12]).CellLabelText, Is.EqualTo("Farrah"));
			Assert.That(((IDefaultCellContext) _compositeCollection[13]).CellLabelText, Is.EqualTo("C"));
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewStartingIndex == 10));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewItems.Count == 3));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[0]).CellLabelText == "F"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[1]).CellLabelText == "Fanny"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[2]).CellLabelText == "Farrah"));
		}

		[Test]
		public void When_GroupReplaced_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			var group = new Group("F") {
				new GroupEntry("Fanny"),
				new GroupEntry("Farrah")
			};
			
			_rawItemSource[2] = group;
			Assert.That(_compositeCollection.Count, Is.EqualTo(20));
			Assert.That(((IDefaultCellContext) _compositeCollection[8]).CellLabelText, Is.EqualTo("Ben"));
			Assert.That(((IDefaultCellContext) _compositeCollection[9]).CellLabelText, Is.EqualTo("Bump"));
			Assert.That(((IDefaultCellContext) _compositeCollection[10]).CellLabelText, Is.EqualTo("F"));
			Assert.That(((IDefaultCellContext) _compositeCollection[11]).CellLabelText, Is.EqualTo("Fanny"));
			Assert.That(((IDefaultCellContext) _compositeCollection[12]).CellLabelText, Is.EqualTo("Farrah"));
			Assert.That(((IDefaultCellContext) _compositeCollection[13]).CellLabelText, Is.EqualTo("D"));
			Assert.That(((IDefaultCellContext) _compositeCollection[14]).CellLabelText, Is.EqualTo("Darren"));
			
			
			Received.InOrder(() => {
				reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove));
				reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add));
			});
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && o.OldStartingIndex == 10));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && o.OldItems.Count == 5));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && ((IDefaultCellContext)o.OldItems[0]).CellLabelText == "C"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && ((IDefaultCellContext)o.OldItems[1]).CellLabelText == "Calvin"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && ((IDefaultCellContext)o.OldItems[2]).CellLabelText == "Calum"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && ((IDefaultCellContext)o.OldItems[3]).CellLabelText == "Collin"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove && ((IDefaultCellContext)o.OldItems[4]).CellLabelText == "Cornelius"));
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add && o.NewStartingIndex == 10));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add && o.NewItems.Count == 3));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add && ((IDefaultCellContext)o.NewItems[0]).CellLabelText == "F"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add && ((IDefaultCellContext)o.NewItems[1]).CellLabelText == "Fanny"));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add && ((IDefaultCellContext)o.NewItems[2]).CellLabelText == "Farrah"));
		}

		[Test]
		public void When_Cleared_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			_rawItemSource.Clear();
			Assert.That(_compositeCollection, Is.Empty);
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Reset));
		}

		[Test]
		public void When_TheCollectionIsModified_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			// Test child insert
			var newEntry = new GroupEntry("Some Test");
			_rawItemSource[0].Insert(3, newEntry);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test Child Add
			_rawItemSource[4].Add(newEntry);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test child Remove
			_rawItemSource[0].RemoveAt(1);
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
			
			// Test child Replace (Replace Bobby with Collin)
			_rawItemSource[1][1] = _rawItemSource[2][2];
			Assert.That(_compositeCollection.Count, Is.EqualTo(22));
			
			// Test delegates
			reciever.DidNotReceive().Recieve(_compositeCollection, Arg.Any<NotifyCollectionChangedEventArgs>());
			reciever.ClearReceivedCalls();
		}
	}
}