using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    internal class ItemContainerData : ScriptableObject
    {
        [SerializeField] private List<ItemData> _itemDatas = new ();

        public IReadOnlyList<ItemData> ItemDatas => _itemDatas;
    }
}