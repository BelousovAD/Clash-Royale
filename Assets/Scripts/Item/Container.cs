using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap;
using UnityEngine;

namespace Item
{
    public class Container : IDisposable
    {
        private const int MinIndex = -1;

        private readonly ContainerData _data;
        private readonly List<Item> _items = new ();
        private int _index = MinIndex;
        private SavvyServicesProvider _services;

        public Container(ContainerData data) =>
            _data = data;

        public event Action ContentChanged;

        public event Action SelectChanged;

        public ContainerType Type => _data.Type;

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
            Unsubscribe();
        
        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Add(Item item)
        {
            if (item.Type.Equals(_data.ItemType) == false)
            {
                Debug.LogError($"Can not add item:{item.Id}. Require type:{_data.ItemType}");
                return;
            }
            
            if (_items.Count >= _items.Capacity)
            {
                Debug.LogError($"Can not add item:{item.Id}. Item list is full");
                return;
            }
            
            item.Selected += SelectById;
            _items.Add(item);
            Save();
            ContentChanged?.Invoke();
        }

        public void Deselect() =>
            Index = MinIndex;

        public void Load()
        {
            Unsubscribe();
            _items.Clear();
            SaveData saveData = _services.Preferences.LoadJson(_data.Type + nameof(_items), new SaveData
            {
                Capacity = _data.DefaultCapacity,
                ItemSubtypes = _data.DefaultDatas.Select(itemData => itemData.Subtype).ToList()
            });
            _items.Capacity = saveData.Capacity;

            foreach (string itemSubtype in saveData.ItemSubtypes)
            {
                ItemData data = _data.AllDatas.FirstOrDefault(itemData => itemData.Subtype == itemSubtype);

                if (data is null)
                {
                    Debug.LogError($"Item:{itemSubtype} not found in {nameof(_data.AllDatas)} of container:{_data.Type}");
                    return;
                }

                Item item = CreateItem(data);
                item.Initialize(_services);
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
                Debug.LogError(
                    $"Can not replace selected item with a new item:{newItem.Id}. Require type:{_data.ItemType}");
                return;
            }

            item.Selected -= SelectById;
            _items.Remove(item);
            newItem.Selected += SelectById;
            _items.Insert(Index, newItem);
            Save();
            ContentChanged?.Invoke();
        }

        public void Remove(Item item)
        {
            item.Selected -= SelectById;
            _items.Remove(item);
            Save();
            ContentChanged?.Invoke();
        }

        protected virtual Item CreateItem(ItemData data) =>
            new (data);

        private void Save()
        {
            SaveData saveData = new ()
            {
                Capacity = _items.Capacity,
                ItemSubtypes = _items.Select(item => item.Subtype).ToList()
            };
            
            _services.Preferences.SaveJson(_data.Type + nameof(_items), saveData);
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
        private struct SaveData
        {
            public int Capacity;
            public List<string> ItemSubtypes;
        }
    }
}