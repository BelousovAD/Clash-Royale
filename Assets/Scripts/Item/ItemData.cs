using System;

namespace Item
{
    using UnityEngine;

    internal abstract class ItemData<T> : ScriptableObject
        where T : Enum
    {
        [SerializeField] private T _type;
        [SerializeField] private Sprite _icon;

        public T Type => _type;
        
        public Sprite Icon => _icon;
    }
}