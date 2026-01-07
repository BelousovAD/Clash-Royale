using System;
using UnityEngine;

namespace Item
{
    public class Item : IEquatable<Item>
    {
        private static int s_id;

        private readonly int _id;
        
        public Item(ItemData data)
        {
            _id = s_id++;
            Data = data;
        }
        
        public event Action<string> Selected;

        public ItemType Type => Data.Type;

        public string Subtype => Data.Subtype;

        public string Id => Type + Subtype + _id;

        public Sprite Icon => Data.Icon;
        
        protected ItemData Data { get; }

        public bool Equals(Item other) =>
            other is not null &&
            other.GetType() == GetType() &&
            Type.Equals(other.Type) &&
            Subtype.Equals(other.Subtype);

        public override bool Equals(object obj) =>
            Equals(obj as Item);

        public override int GetHashCode() =>
            Id.GetHashCode();
        
        public void Select() =>
            Selected?.Invoke(Id);
    }
}