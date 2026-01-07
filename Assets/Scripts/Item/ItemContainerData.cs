using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    internal abstract class ItemContainerData<T> : ScriptableObject
        where T : Enum
    {
        [SerializeField] private List<ItemData<T>> _itemDatas = new ();

        public IReadOnlyList<ItemData<T>> ItemDatas => _itemDatas;
    }
}