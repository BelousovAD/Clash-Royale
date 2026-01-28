using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap;
using UnityEngine;

namespace Item
{
    public class Container : IDisposable
    {
        protected const int DefaultId = -1;
        private const int MinIndex = -1;

        private readonly ContainerData _data;
        private readonly List<Item> _items = new ();
        private int _index = MinIndex;

        public Container(ContainerData data) =>
            _data = data;

        public event Action ContentChanged;

        public event Action SelectChanged;

        public int Capacity => _items.Capacity;
        
        public ContainerType Type => _data.Type;

        public Item Selected => Index > MinIndex ? Items[Index] : null;

        public IReadOnlyList<Item> Items => _items;

        public int Index
        {
            get
            {
                return _index;
            }
            
            private set
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

        protected SavvyServicesProvider Services { get; private set; }

        public virtual void Dispose()
        {
            Deselect();
            Unsubscribe();
            _items.ForEach(item => item.Dispose());
            _items.Clear();
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            Services = servicesProvider;

        public void Add(Item item)
        {
            if (item.Type != _data.ItemType)
            {
                Debug.LogError($"Can not add item. Require type:{_data.ItemType}");
                return;
            }
            
            if (_items.Count >= _items.Capacity)
            {
                Debug.LogError($"Can not add item. Item list is full");
                return;
            }
            
            item.Initialize(Services);
            _items.Add(item);
            item.UpdateId(_items.Count - 1);
            item.Selected += SelectById;
            ContentChanged?.Invoke();
        }

        public void Deselect() =>
            Index = MinIndex;
        
        public void Load()
        {
            Dispose();
            SerializableData serializableData = GetSerializableData();
            _items.Capacity = serializableData.Capacity;

            for (int i = 0; i < serializableData.ItemSubtypes.Count; i++)
            {
                string itemSubtype = serializableData.ItemSubtypes[i];
                ItemData data = _data.AllDatas.FirstOrDefault(itemData => itemData.Subtype == itemSubtype);

                if (data is null)
                {
                    Debug.LogError(
                        $"Item:{itemSubtype} not found in {nameof(_data.AllDatas)} of container:{_data.Type}");
                    return;
                }

                Item item = CreateItem(data, i);
                item.Initialize(Services);
                _items.Add(item);
                item.Load();
            }

            Subscribe();
        }

        public void ReplaceSelected(Item newItem)
        {
            Item item = Selected;
            
            if (item is null)
            {
                Debug.LogError($"Can not replace. Selected item is null");
                return;
            }

            if (newItem.Type.Equals(_data.ItemType) == false)
            {
                Debug.LogError($"Can not replace selected item with a new item. Require type:{_data.ItemType}");
                return;
            }

            item.Selected -= SelectById;
            _items.Remove(item);
            _items.Insert(Index, newItem);
            newItem.UpdateId(Index);
            newItem.Selected += SelectById;
            ContentChanged?.Invoke();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            Item item = _items[index];
            item.Selected -= SelectById;
            _items.RemoveAt(index);

            for (int i = index; i < _items.Count; i++)
            {
                _items[i].UpdateId(i);
            }
            
            ContentChanged?.Invoke();
        }

        public virtual Item CreateItem(ItemData data, int id = DefaultId) =>
            new (data, id);

        protected virtual SerializableData GetSerializableData()
        {
            return new SerializableData
            {
                Capacity = _data.DefaultCapacity,
                ItemSubtypes = _data.DefaultDatas.Select(itemData => itemData.Subtype).ToList(),
            };
        }

        private void SelectById(int id)
        {
            if (id > MinIndex)
            {
                Index = id;
            }
            else
            {
                Debug.LogError($"Can not select item with id:{id}");
            }
        }       

        private void Subscribe()
        {
            foreach (Item item in _items)
            {
                item.Selected += SelectById;
            }
        }

        private void Unsubscribe()
        {
            foreach (Item item in Items)
            {
                item.Selected -= SelectById;
            }
        }

        [Serializable]
        protected struct SerializableData
        {
            public int Capacity;
            public List<string> ItemSubtypes;
        }
    }
}