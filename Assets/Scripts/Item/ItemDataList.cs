using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = nameof(ItemDataList), menuName = nameof(Item) + "/" + nameof(ItemDataList))]
    public class ItemDataList : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private List<ItemData> _itemDatas = new ();

        public ItemType Type => _type;
        
        public IReadOnlyList<ItemData> ItemDatas => _itemDatas;

        private void OnValidate()
        {
            for (int i = 0; i < ItemDatas.Count; i++)
            {
                ItemType itemType = ItemDatas[i].Type;
                
                if (itemType != Type)
                {
                    Debug.LogError($"Element at:{i} have type:{itemType}. Require type:{Type}");
                    return;
                }
            }
        }
    }
}