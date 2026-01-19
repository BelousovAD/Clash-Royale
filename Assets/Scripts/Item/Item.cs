using System;
using Bootstrap;
using UnityEngine;

namespace Item
{
    public class Item : IDisposable
    {
        protected const int DefaultId = -1;
        
        public Item(ItemData data, int id = DefaultId)
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

        public void UpdateId(int id = DefaultId)
        {
            if (id < DefaultId)
            {
                throw new ArgumentOutOfRangeException(nameof(id), $"Can not be less than {DefaultId}");
            }
            
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