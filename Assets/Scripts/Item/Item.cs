using System;
using UnityEngine;

namespace Item
{
    internal abstract class Item<T> : IEquatable<Item<T>>
        where T : Enum
    {
        private static int s_id;

        private readonly int _id;
        
        public Item(ItemData<T> data)
        {
            _id = s_id++;
            Data = data;
        }
        
        public event Action<string> Selected;

        public T Type => Data.Type;

        public string Id => Type.ToString() + _id;

        public Sprite Icon => Data.Icon;
        
        protected ItemData<T> Data { get; }

        public bool Equals(Item<T> other) =>
            other is not null && other.GetType() == GetType() && Type.Equals(other.Type);

        public override bool Equals(object obj) =>
            Equals(obj as Item<T>);

        public override int GetHashCode() =>
            Id.GetHashCode();
        
        public void Select() =>
            Selected?.Invoke(Id);
    }
}