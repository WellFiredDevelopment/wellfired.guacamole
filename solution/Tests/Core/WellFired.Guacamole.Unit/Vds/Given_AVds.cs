using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Vds
{
    [TestFixture]
    public class Given_AVds
    {
        [Test]
        public void With_NoEntriesAllNew_CorrectCallbacksOccur()
        {
            var oldVds = new int [0];
            var newVds = new [] { 0, 1, 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(Arg.Any<int>());
            Received.InOrder(() => {
                listensToVdsChanges.ItemEnteredVds(0, false);
                listensToVdsChanges.ItemEnteredVds(1, false);
                listensToVdsChanges.ItemEnteredVds(2, false);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_OneEntryAllNew_CorrectCallbacksOccur()
        {
            var oldVds = new [] { 0 };
            var newVds = new [] { 0, 1, 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(Arg.Any<int>());
            Received.InOrder(() => {
                listensToVdsChanges.ItemEnteredVds(1, false);
                listensToVdsChanges.ItemEnteredVds(2, false);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_OneEntryOneRemoved_CorrectCallbacksOccur()
        {
            var oldVds = new [] { 0 };
            var newVds = new [] { 1, 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(0);
                listensToVdsChanges.ItemEnteredVds(1, false);
                listensToVdsChanges.ItemEnteredVds(2, false);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_ThreeEntriesTwoRemoved_CorrectCallbacksOccur()
        {
            var oldVds = new [] { 0, 1, 2 };
            var newVds = new [] { 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(2);
            listensToVdsChanges.DidNotReceive().ItemEnteredVds(2, Arg.Any<bool>());
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(0);
                listensToVdsChanges.ItemLeftVds(1);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_TwoNewEntriesAtTheFront_CorrectCallbacksOccur()
        {
            var oldVds = new [] { 2, 3, 4 };
            var newVds = new [] { 0, 1, 2 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(2);
            listensToVdsChanges.DidNotReceive().ItemEnteredVds(2, Arg.Any<bool>());
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(3);
                listensToVdsChanges.ItemLeftVds(4);
                listensToVdsChanges.ItemEnteredVds(1, true);
                listensToVdsChanges.ItemEnteredVds(0, true);
            });
        }
        
        [Test]
        public void With_TwoEntriesInTheOld_And_BothAreReplacedWithHigher_CorrectCallbacksOccur()
        {
            var oldVds = new [] { 3, 4 };
            var newVds = new [] { 6, 7 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(3);
                listensToVdsChanges.ItemLeftVds(4);
                listensToVdsChanges.ItemEnteredVds(6, false);
                listensToVdsChanges.ItemEnteredVds(7, false);
            });
        }
        
        [Test]
        public void With_TwoEntriesInTheOld_And_BothAreReplacedWithLower_CorrectCallbacksOccur()
        {
            var oldVds = new [] { 3, 4 };
            var newVds = new [] { 0, 1 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(3);
                listensToVdsChanges.ItemLeftVds(4);
                listensToVdsChanges.ItemEnteredVds(1, true);
                listensToVdsChanges.ItemEnteredVds(0, true);
            });
        }
    }
}