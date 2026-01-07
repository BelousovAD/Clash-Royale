using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = nameof(ItemContainerData), menuName = nameof(Item) + "/" + nameof(ItemContainerData))]
    internal class ItemContainerData : ScriptableObject
    {
        [SerializeField] private List<ItemData> _itemDatas = new ();

        public IReadOnlyList<ItemData> ItemDatas => _itemDatas;
    }
}