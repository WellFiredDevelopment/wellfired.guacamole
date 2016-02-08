using System;
using System.Collections;
using System.Collections.Generic;

namespace WellFired.Guacamole.Databinding
{
    public class ObservableList<T> : IList<T>
    {
        protected List<T> InternalList = new List<T>();

		public event Action<T> OnAdd = delegate(T obj) { };
        public event Action<int, T> OnInset = delegate(int arg1, T arg2) { };
        public event Action<T> OnRemove = delegate(T obj) { };

		#pragma warning disable 67
		public event Action OnClear = delegate { };
		#pragma warning restore 67

        public IEnumerator<T> GetEnumerator()
        {
            return InternalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return InternalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            InternalList.Insert(index, item);
            OnInset(index, item);
        }

        public void RemoveAt(int index)
        {
            InternalList.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return InternalList[index]; }
            set { InternalList[index] = value; }
        }

        public void Add(T item)
        {
            InternalList.Add(item);
			OnAdd(item);
        }

        public void Clear()
        {
            InternalList.Clear();
            OnClear();
        }

        public bool Contains(T item)
        {
            return InternalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InternalList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return InternalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            if(InternalList.Remove(item))
            {
				OnRemove(item);
                return true;
            }

            return false;
        }
    }
}