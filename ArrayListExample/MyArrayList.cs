using _ArrayList;
using GSF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayListExample
{
    public class MyArrayList
    {
        private object[] _items;
        private int _size;
        private int _version;
        private const int _defaultCapacity = 4;
        private static readonly Object[] _emptyArray = GSF.EmptyArray<Object>.Empty;

        public MyArrayList()
        {
            _items = _emptyArray;
        }
        public MyArrayList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            else if (capacity == 0)
                _items = _emptyArray;
            else
                _items = new Object[capacity];
        }
        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0); // nayel
                return _size;
            }
        }
        public int Capacity
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= Count);
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                Contract.Ensures(Capacity >= 0);
                Contract.EndContractBlock();  // nayel
                
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        Object[] newItems = new Object[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = new Object[_defaultCapacity];
                    }
                }
            }
        }
        public object this[int index]
        {
            get 
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException();
                Contract.EndContractBlock();
                return _items[index];
            }
            set 
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException();
                Contract.EndContractBlock(); // nayel
                _items[index] = value;
                _version++;
            }
        }
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > uint.MaxValue) newCapacity = int.MaxValue;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public int Add(Object value)
        {
            Contract.Ensures(Contract.Result<int>() >= 0); // nayel
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size] = value;
            _version++;
            return _size++;
        }
        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }
        public Object Clone()
        {
            Contract.Ensures(Contract.Result<Object>() != null);
            MyArrayList la = new MyArrayList(_size);
            la._size = _size;
            la._version = _version;
            Array.Copy(_items, 0, la._items, 0, _size);
            return la;
        }
        public void CopyTo(Array array)
        {
            CopyTo(array, 0);
        }
        public void CopyTo(Array array, int arrayIndex)
        {
            if ((array != null) && (array.Rank != 1))
                throw new ArgumentException();
            Contract.EndContractBlock();
            Array.Copy(_items, 0, array, arrayIndex, _size);
        }
        public int IndexOf(Object value)
        {
            Contract.Ensures(Contract.Result<int>() < Count);
            return Array.IndexOf((Array)_items, value, 0, _size);
        }
        public virtual void Insert(int index, Object value)
        {
            if (index < 0 || index > _size) throw new ArgumentOutOfRangeException();
            Contract.EndContractBlock();

            if (_size == _items.Length) EnsureCapacity(_size + 1);
            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = value;
            _size++;
            _version++;
        }
        public void Reverse(int index, int count)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (_size - index < count)
                throw new ArgumentException();
            Contract.EndContractBlock();
            Array.Reverse(_items, index, count);
            _version++;
        }
        public virtual void Sort(int index, int count, IComparer comparer)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (_size - index < count)
                throw new ArgumentException();
            Contract.EndContractBlock();

            Array.Sort(_items, index, count, comparer);
            _version++;
        }
    }
}