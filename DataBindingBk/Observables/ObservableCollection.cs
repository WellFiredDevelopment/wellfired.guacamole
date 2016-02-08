using System;
using System.Collections.Generic;
using System.Linq;

namespace WellFired.Guacamole.Databinding
{
    public interface IObservableCollection
    {
        event Action<object> OnObjectAdd;
        event Action<object> OnObjectRemove;
        event Action<int, object> OnObjectInsert;
        event Action OnClear;
        IEnumerable<object> GetObjects();
    }
		
    public class ObservableCollection<T> : IEnumerable<T>, IObservableCollection
    {
		private Action<object> onObjectAdd = delegate { };
		private Action<int, object> onObjectInsert = delegate { };
		private Action<object> onObjectRemove = delegate { };
		private Action<T> onAdd = delegate { };
		private Action<int,T> onInsert = delegate { };
		private Action<T> onRemove = delegate { };
		private Action onClear = delegate { };
		private readonly List<T> list = new List<T>();

        public event Action<object> OnObjectAdd
        {
            add
            {
                onObjectAdd = (Action<object>)Delegate.Combine(onObjectAdd, value);
            }
            remove
            {
                onObjectAdd = (Action<object>)Delegate.Remove(onObjectAdd, value);
            }
        }

        public event Action<int, object> OnObjectInsert
        {
            add
            {
                onObjectInsert = (Action<int, object>)Delegate.Combine(onObjectInsert, value);
            }
            remove
            {
                onObjectInsert = (Action<int, object>)Delegate.Remove(onObjectInsert, value);
            }
        }

        public event Action<object> OnObjectRemove
        {
            add
            {
                onObjectRemove = (Action<object>)Delegate.Combine(onObjectRemove, value);
            }
            remove
            {
                onObjectRemove = (Action<object>)Delegate.Remove(onObjectRemove, value);
            }
        }
        
        public event Action<T> OnAdd
        {
            add
            {
                onAdd = (Action<T>)Delegate.Combine(onAdd, value);
            }
            remove
            {
                onAdd = (Action<T>)Delegate.Remove(onAdd, value);
            }
        }
        
        public event Action<int, T> OnInsert
        {
            add
            {
                onInsert = (Action<int, T>)Delegate.Combine(onInsert, value);
            }
            remove
            {
                onInsert = (Action<int, T>)Delegate.Remove(onInsert, value);
            }
        }

        public event Action<T> OnRemove
        {
            add
            {
                onRemove = (Action<T>)Delegate.Combine(onRemove, value);
            }
            remove
            {
                onRemove = (Action<T>)Delegate.Remove(onRemove, value);
            }
        }
        
        public event Action OnClear
        {
            add
            {
                onClear = (Action)Delegate.Combine(onClear, value);
            }
            remove
            {
                onClear = (Action)Delegate.Remove(onClear, value);
            }
        }

        public ObservableCollection()
        {

        }

        public ObservableCollection(IEnumerable<T> set)
        {
            Add(set);
        }

        public IEnumerable<object> GetObjects()
        {
            if (list.Count == 0)
                return new object[0];
            return list.Cast<object>();
        }

        public T this[int i]
        {
            get { return list[i]; }
            set { list[i] = value; }
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void Add(T o)
        {
            list.Add(o);

            if (onAdd != null)
                onAdd(o);

            if (onObjectAdd != null)
                onObjectAdd(o);
        }

        public void Add(IEnumerable<T> o)
        {
            var s = o.ToArray();

            for (int i = 0;i < s.Length;i++)
            {
                Add(s[i]);
            }
        }

        public void Remove(T o)
        {
            if (list.Remove(o))
            {
                if (onRemove != null)
                    onRemove(o);

                if (onObjectRemove != null)
                    onObjectRemove(o);
            }
        }

        public void Remove(IEnumerable<T> o)
        {
            var s = o.ToArray();

            for (int i = 0;i < s.Length;i++)
            {
                Remove(s[i]);
            }
        }

        public void Insert(int index, T o)
        {
            list.Insert(index, o);

            if (onInsert != null)
                onInsert(index, o);

            if (onInsert != null)
                onObjectInsert(index, o);

        }

        public void Clear()
        {
            list.Clear();

            if (onClear != null)
                onClear();
        }

        public void Release()
        {
			Clear();
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T[] ToArray()
        {
            return list.ToArray();
        }
    }
}