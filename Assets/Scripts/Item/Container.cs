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
        
        public virtual void Dispose()
        {
            Deselect();
            Unsubscribe();
            _items.ForEach(item => item.Dispose());
            _items.Clear();
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Add(Item item)
        {
            if (item.Type.Equals(_data.ItemType) == false)
            {
                Debug.LogError($"Can not add item. Require type:{_data.ItemType}");
                return;
            }
            
            if (_items.Count >= _items.Capacity)
            {
                Debug.LogError($"Can not add item. Item list is full");
                return;
            }
            
            item.Selected += SelectById;
            item.Initialize(_services);
            item.UpdateId(_items.Count);
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

            for (int i = 0; i < saveData.ItemSubtypes.Count; i++)
            {
                string itemSubtype = saveData.ItemSubtypes[i];
                ItemData data = _data.AllDatas.FirstOrDefault(itemData => itemData.Subtype == itemSubtype);

                if (data is null)
                {
                    Debug.LogError(
                        $"Item:{itemSubtype} not found in {nameof(_data.AllDatas)} of container:{_data.Type}");
                    return;
                }

                Item item = CreateItem(data, i);
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
                Debug.LogError($"Can not replace selected item with a new item. Require type:{_data.ItemType}");
                return;
            }

            item.Selected -= SelectById;
            _items.Remove(item);
            newItem.Selected += SelectById;
            _items.Insert(Index, newItem);
            newItem.UpdateId(Index);
            Save();
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
            
            Save();
            ContentChanged?.Invoke();
        }

        protected virtual Item CreateItem(ItemData data, int id) =>
            new (data, id);

        private void Save()
        {
            SaveData saveData = new ()
            {
                Capacity = _items.Capacity,
                ItemSubtypes = _items.Select(item => item.Subtype).ToList()
            };
            
            _services.Preferences.SaveJson(_data.Type + nameof(_items), saveData);
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
        private struct SaveData
        {
            public int Capacity;
            public List<string> ItemSubtypes;
        }
    }
}