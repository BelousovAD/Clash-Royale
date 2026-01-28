using System;
using Bootstrap;
using UnityEngine;

namespace Item
{
    public class Item : IDisposable
    {
        public Item(ItemData data, int id)
        {
            Data = data;
            Id = id;
        }

        public event Action<int> Selected;

        public ItemType Type => Data.Type;

        public string Subtype => Data.Subtype;

        public int Id { get; private set; }

        public Sprite Icon => Data.Icon;

        protected ItemData Data { get; }

        protected SavvyServicesProvider Services { get; private set; }

        public virtual void Initialize(SavvyServicesProvider servicesProvider) =>
            Services = servicesProvider;

        public virtual void Load()
        { }

        public void UpdateId(int id)
        {
            DeleteSaves();
            Id = id;
            Save();
        }

        public void Select() =>
            Selected?.Invoke(Id);

        public virtual void Dispose()
        { }

        protected virtual void DeleteSaves()
        { }

        protected virtual void Save()
        { }
    }
}