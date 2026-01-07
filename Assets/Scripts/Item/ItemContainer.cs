using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    internal abstract class ItemContainer<T> : IDisposable
        where T : Enum
    {
        private const int MinIndex = -1;
        
        private readonly List<Item<T>> _items;
        private int _index = MinIndex;

        public ItemContainer(int capacity = 0) =>
            _items = new List<Item<T>>(capacity);

        public event Action ContentChanged;

        public event Action SelectChanged;

        public int Capacity => _items.Capacity;

        public Item<T> Selected => Index > MinIndex ? Items[Index] : null;

        public IReadOnlyList<Item<T>> Items => _items;

        private int Index
        {
            get
            {
                return _index;
            }
            
            set
            {
                if (_items.Count == 0)
                {
                    _index = MinIndex;
                    SelectChanged?.Invoke();
                    return;
                }

                _index = Mathf.Clamp(value, MinIndex, _items.Count - 1);
                SelectChanged?.Invoke();
            }
        }
        
        public virtual void Dispose() =>
            Clear();

        public void Add(Item<T> item)
        {
            if (_items.Count < Capacity)
            {
                item.Selected += SelectById;
                _items.Add(item);
                ContentChanged?.Invoke();
            }
        }

        public void Clear()
        {
            foreach (Item<T> item in Items)
            {
                item.Selected -= SelectById;
            }
            
            Index = MinIndex;
            _items.Clear();
            ContentChanged?.Invoke();
        }
        
        public void RemoveSelected()
        {
            Item<T> item = Selected;

            if (item is not null)
            {
                Remove(item);
                Index -= 1;
            }
        }
        
        private void Remove(Item<T> item)
        {
            item.Selected -= SelectById;
            _items.Remove(item);
            ContentChanged?.Invoke();
        }

        private void SelectById(string id)
        {
            int index = _items.FindIndex(item => item.Id == id);

            if (index > MinIndex)
            {
                Index = index;
            }
            else
            {
                Debug.LogError($"Can not select item with id:{id}");
            }
        }
    }
}