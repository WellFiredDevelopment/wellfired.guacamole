using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WellFired.Guacamole.Data.Collection
{
    [Serializable]
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        // Fields
        private readonly SimpleMonitor _monitor;

        public PropertyChangedEventHandler PropertyChangedDelegate { get; private set; }
        private readonly object _propertyChangedDelegateLockObject = new object();

        // Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                lock (_propertyChangedDelegateLockObject)
                {
                    PropertyChangedDelegate += value;
                }
            }
            remove
            {
                lock (_propertyChangedDelegateLockObject)
                {
                    // ReSharper disable once DelegateSubtraction
                    PropertyChangedDelegate -= value;
                }
            }
        }

        // Methods
        public ObservableCollection()
        {
            _monitor = new SimpleMonitor();
        }

        // ReSharper disable once AssignNullToNotNullAttribute
        public ObservableCollection(List<T> list) : base(list != null ? new List<T>(list.Count) : list)
        {
            _monitor = new SimpleMonitor();
            var items = Items;
            if (list == null)
                return;

            using (IEnumerator<T> enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    items.Add(enumerator.Current);
                }
            }
        }

        private IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        private void CheckReentrancy()
        {
            if (_monitor.Busy && CollectionChanged != null && CollectionChanged.GetInvocationList().Length > 1)
            {
                throw new InvalidOperationException("ObservableCollection Reentrancy Not Allowed");
            }
        }

        protected override void ClearItems()
        {
            CheckReentrancy();
            base.ClearItems();
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionReset();
        }

        protected override void InsertItem(int index, T item)
        {
            CheckReentrancy();
            base.InsertItem(index, item);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        public void Move(int oldIndex, int newIndex)
        {
            MoveItem(oldIndex, newIndex);
        }

        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            CheckReentrancy();
            var item = base[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged == null)
                return;

            using (BlockReentrancy())
            {
                CollectionChanged?.Invoke(this, e);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected override void RemoveItem(int index)
        {
            CheckReentrancy();
            var item = base[index];
            base.RemoveItem(index);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        protected override void SetItem(int index, T item)
        {
            CheckReentrancy();
            var oldItem = base[index];
            base.SetItem(index, item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
        }

        // Nested Types
        [Serializable]
        private class SimpleMonitor : IDisposable
        {
            // Fields
            private int _busyCount;

            // Methods
            public void Dispose()
            {
                _busyCount--;
            }

            public void Enter()
            {
                _busyCount++;
            }

            // Properties
            public bool Busy => _busyCount > 0;
        }
    }

    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        // Fields

        // Methods
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("WrongActionForCtor. expected: NotifyCollectionChangedAction.Reset ", nameof(action));
            }
            InitializeAdd(action, null, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", nameof(action));
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", nameof(action));
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                {
                    throw new ArgumentNullException(nameof(changedItems));
                }
                InitializeAddOrRemove(action, changedItems, -1);
            }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", nameof(action));
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItem != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", nameof(action));
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                InitializeAddOrRemove(action, new[] { changedItem }, -1);
            }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor. expected: Replace", nameof(action));
            }
            if (newItems == null)
            {
                throw new ArgumentNullException(nameof(newItems));
            }
            if (oldItems == null)
            {
                throw new ArgumentNullException(nameof(oldItems));
            }
            InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", nameof(action));
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", nameof(action));
                }
                if (startingIndex != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1", nameof(action));
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                {
                    throw new ArgumentNullException(nameof(changedItems));
                }
                if (startingIndex < -1)
                {
                    throw new ArgumentException("IndexCannotBeNegative", nameof(startingIndex));
                }
                InitializeAddOrRemove(action, changedItems, startingIndex);
            }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor", nameof(action));
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItem != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem", nameof(action));
                }
                if (index != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1", nameof(action));
                }
                InitializeAdd(action, null, -1);
            }
            else
            {
                InitializeAddOrRemove(action, new[] { changedItem }, index);
            }
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor expected: replace", nameof(action));
            }
            InitializeMoveOrReplace(action, new[] { newItem }, new[] { oldItem }, -1, -1);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor expected: replace", nameof(action));
            }
            if (newItems == null)
            {
                throw new ArgumentNullException(nameof(newItems));
            }
            if (oldItems == null)
            {
                throw new ArgumentNullException(nameof(oldItems));
            }
            InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Move)
            {
                throw new ArgumentException("WrongActionForCtor", nameof(action));
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative", nameof(index));
            }
            InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Move)
            {
                throw new ArgumentException("WrongActionForCtor", nameof(action));
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative", nameof(index));
            }
            var newItems = new[] { changedItem };
            InitializeMoveOrReplace(action, newItems, newItems, index, oldIndex);
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
        {
            NewStartingIndex = -1;
            OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor", nameof(action));
            }
            InitializeMoveOrReplace(action, new[] { newItem }, new[] { oldItem }, index, index);
        }

        private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
        {
            Action = action;
            NewItems = newItems == null ? null : ArrayList.ReadOnly(newItems);
            NewStartingIndex = newStartingIndex;
        }

        private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            switch (action)
            {
                case NotifyCollectionChangedAction.Add:
                    InitializeAdd(action, changedItems, startingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    InitializeRemove(action, changedItems, startingIndex);
                    break;
                default:
                    throw new ArgumentException("unsupported action in this case", nameof(action));
            }
        }

        private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
        {
            InitializeAdd(action, newItems, startingIndex);
            InitializeRemove(action, oldItems, oldStartingIndex);
        }

        private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
        {
            Action = action;
            OldItems = oldItems == null ? null : ArrayList.ReadOnly(oldItems);
            OldStartingIndex = oldStartingIndex;
        }

        // Properties
        public NotifyCollectionChangedAction Action { get; private set; }

        public IList NewItems { get; private set; }

        public int NewStartingIndex { get; private set; }

        public IList OldItems { get; private set; }

        public int OldStartingIndex { get; private set; }
    }

    public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);
    public interface INotifyCollectionChanged
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
    public enum NotifyCollectionChangedAction
    {
        Add,
        Remove,
        Replace,
        Move,
        Reset
    }
}