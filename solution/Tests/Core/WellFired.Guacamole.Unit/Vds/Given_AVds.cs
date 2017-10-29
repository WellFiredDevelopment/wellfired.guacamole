using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Vds
{
    [TestFixture]
    public class GivenAVds
    {
        [Test]
        public void With_NoEntriesAllNew_CorrectCallbacksOccur()
        {
            var oldVds = new List<int>();
            var newVds = new List<int> { 0, 1, 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(Arg.Any<int>(), Arg.Any<bool>());
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
            var oldVds = new List<int> { 0 };
            var newVds = new List<int> { 0, 1, 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(Arg.Any<int>(), Arg.Any<bool>());
            Received.InOrder(() => {
                listensToVdsChanges.ItemEnteredVds(1, false);
                listensToVdsChanges.ItemEnteredVds(2, false);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_OneEntryOneRemoved_CorrectCallbacksOccur()
        {
            var oldVds = new List<int> { 0 };
            var newVds = new List<int> { 1, 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(0, Arg.Any<bool>());
                listensToVdsChanges.ItemEnteredVds(1, false);
                listensToVdsChanges.ItemEnteredVds(2, false);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_ThreeEntriesTwoRemoved_CorrectCallbacksOccur()
        {
            var oldVds = new List<int> { 0, 1, 2 };
            var newVds = new List<int> { 2, 3 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(2, Arg.Any<bool>());
            listensToVdsChanges.DidNotReceive().ItemEnteredVds(2, Arg.Any<bool>());
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(0, true);
                listensToVdsChanges.ItemLeftVds(1, true);
                listensToVdsChanges.ItemEnteredVds(3, false);
            });
        }
        
        [Test]
        public void With_TwoNewEntriesAtTheFront_CorrectCallbacksOccur()
        {
            var oldVds = new List<int> { 2, 3, 4 };
            var newVds = new List<int> { 0, 1, 2 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            listensToVdsChanges.DidNotReceive().ItemLeftVds(2, Arg.Any<bool>());
            listensToVdsChanges.DidNotReceive().ItemEnteredVds(2, Arg.Any<bool>());
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(3, Arg.Any<bool>());
                listensToVdsChanges.ItemLeftVds(4, Arg.Any<bool>());
                listensToVdsChanges.ItemEnteredVds(1, true);
                listensToVdsChanges.ItemEnteredVds(0, true);
            });
        }
        
        [Test]
        public void With_TwoEntriesInTheOld_And_BothAreReplacedWithHigher_CorrectCallbacksOccur()
        {
            var oldVds = new List<int> { 3, 4 };
            var newVds = new List<int> { 6, 7 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(3, Arg.Any<bool>());
                listensToVdsChanges.ItemLeftVds(4, Arg.Any<bool>());
                listensToVdsChanges.ItemEnteredVds(6, false);
                listensToVdsChanges.ItemEnteredVds(7, false);
            });
        }
        
        [Test]
        public void With_TwoEntriesInTheOld_And_BothAreReplacedWithLower_CorrectCallbacksOccur()
        {
            var oldVds = new List<int> { 3, 4 };
            var newVds = new List<int> { 0, 1 };
            var listensToVdsChanges = Substitute.For<IListensToVdsChanges>();
            
            VdsCalculator.AdjustForNewVds(oldVds, newVds, listensToVdsChanges);
            
            Received.InOrder(() => {
                listensToVdsChanges.ItemLeftVds(3, Arg.Any<bool>());
                listensToVdsChanges.ItemLeftVds(4, Arg.Any<bool>());
                listensToVdsChanges.ItemEnteredVds(1, true);
                listensToVdsChanges.ItemEnteredVds(0, true);
            });
        }
    }
}