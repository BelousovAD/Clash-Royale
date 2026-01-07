using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    internal class ItemContainer : IDisposable
    {
        private const int MinIndex = -1;
        
        private readonly List<Item> _items;
        private int _index = MinIndex;

        public ItemContainer(ItemType type, int capacity = 0)
        {
            Type = type;
            _items = new List<Item>(capacity);
        }

        public event Action ContentChanged;

        public event Action SelectChanged;
        
        public ItemType Type { get; }

        public int Capacity => _items.Capacity;

        public Item Selected => Index > MinIndex ? Items[Index] : null;

        public IReadOnlyList<Item> Items => _items;

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

        public void Add(Item item)
        {
            if (item.Type.Equals(Type) == false)
            {
                Debug.LogError($"Can not add item:{item.Id}. Require type:{Type}");
                return;
            }
            
            if (_items.Count >= Capacity)
            {
                Debug.LogError($"Can not add item:{item.Id}. Item list is full");
                return;
            }
            
            item.Selected += SelectById;
            _items.Add(item);
            ContentChanged?.Invoke();
        }

        public void Clear()
        {
            foreach (Item item in Items)
            {
                item.Selected -= SelectById;
            }
            
            Index = MinIndex;
            _items.Clear();
            ContentChanged?.Invoke();
        }
        
        public void RemoveSelected()
        {
            Item item = Selected;

            if (item is not null)
            {
                Remove(item);
                Index -= 1;
            }
        }
        
        private void Remove(Item item)
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