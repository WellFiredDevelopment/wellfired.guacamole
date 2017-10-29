using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
	[TestFixture]
	public class WithOneTierDataObservable
	{
		private Data.CompositeCollection _compositeCollection;
		private ObservableCollection<GroupEntry> _rawItemSource;

		[SetUp]
		public void SetUp()
		{
			_rawItemSource = new ObservableCollection<GroupEntry> {
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
			Assert.That(_compositeCollection.Count, Is.EqualTo(0));
			
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Reset));
		}

		[Test]
		public void When_TheCollectionIsModified_Then_ModificationIsPropogated()
		{
			var reciever = Substitute.For<IEventMockReciever>();
			_compositeCollection.CollectionChanged += reciever.Recieve;
			
			// Test child insert
			var newEntry = new GroupEntry("Some Test");
			_rawItemSource.Insert(3, newEntry);
			Assert.That(_compositeCollection[3], Is.EqualTo(newEntry));
			
			// Test delegates
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewItems.Count == 1));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewStartingIndex == 3));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[0]).CellLabelText == "Some Test"));
			reciever.ClearReceivedCalls();
			
			// Test Child Add
			_rawItemSource.Add(newEntry);
			Assert.That(_compositeCollection[_compositeCollection.Count - 1], Is.EqualTo(newEntry));
			
			// Test delegates
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Add));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewItems.Count == 1));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewStartingIndex == 18));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[0]).CellLabelText == "Some Test"));
			reciever.ClearReceivedCalls();
			
			// Test child Remove
			_rawItemSource.RemoveAt(1);
			Assert.That(((IDefaultCellContext)_compositeCollection[1]).CellLabelText, Is.Not.EqualTo("Alfie"));
			
			// Test delegates
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Remove));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.OldItems.Count == 1));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.OldStartingIndex == 1));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.OldItems[0]).CellLabelText == "Alfie"));
			reciever.ClearReceivedCalls();
			
			// Test child Replace (Replace Bobby with Collin)
			_rawItemSource[5] = _rawItemSource[10];
			Assert.That(((IDefaultCellContext)_compositeCollection[5]).CellLabelText, Is.EqualTo("Collin"));
			
			// Test delegates
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.Action == NotifyCollectionChangedAction.Replace));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewStartingIndex == 5));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => o.NewItems.Count == 1));
			reciever.Received(1).Recieve(_compositeCollection, Arg.Is<NotifyCollectionChangedEventArgs>(o => ((IDefaultCellContext)o.NewItems[0]).CellLabelText == "Collin"));
			reciever.ClearReceivedCalls();
		}
	}
}