using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CST.LePoint.Tools
{
    public class HashSetSerializable<T> : ICollection<T>
    {
        private readonly HashSet<T> internalSet;

        public HashSetSerializable()
        {
            this.internalSet = new HashSet<T>();
        }

        public void Clear()
        {
            var arr = internalSet.ToList();
            foreach (T t in arr)
            {
                internalSet.Remove(t);
            }
        }

        public bool Contains(T item)
        {
            return internalSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            internalSet.CopyTo(array, arrayIndex);
        }

        public void Add(T item)
        {
            bool b = internalSet.Add(item);
            if (b)
                OnItemAdded(item);
        }

        public bool Remove(T item)
        {
            bool b = internalSet.Remove(item);
            if (b)
                OnItemRemoved(item);
            return b;
        }

        public int Count
        {
            get { return internalSet.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event Action<T> ItemRemoved;

        public event Action<T> ItemAdded;

        public virtual void OnItemRemoved(T item)
        {
            if (ItemRemoved != null) ItemRemoved(item);
        }

        public virtual void OnItemAdded(T item)
        {
            if (ItemAdded != null) ItemAdded(item);
        }
    }
}