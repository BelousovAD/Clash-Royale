using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = nameof(ContainerData), menuName = nameof(Item) + "/" + nameof(ContainerData))]
    public class ContainerData : ScriptableObject
    {
        [SerializeField] private ContainerType _type;
        [SerializeField] private ItemType _itemType;
        [SerializeField][Min(0)] private int _defaultCapacity;
        [SerializeField] private ItemDataList _defaultList;
        [SerializeField] private ItemDataList _fullList;

        public ContainerType Type => _type;

        public ItemType ItemType => _itemType;

        public int DefaultCapacity => _defaultCapacity;

        public IReadOnlyList<ItemData> DefaultDatas => _defaultList.ItemDatas;

        public IReadOnlyList<ItemData> AllDatas => _fullList.ItemDatas;

        private void OnValidate()
        {
            if (_defaultList is not null && _defaultList.Type != ItemType)
            {
                Debug.LogError($"Require {nameof(_defaultList)} with type:{ItemType}");
                _defaultList = null;
                return;
            }
            
            if (_fullList is not null && _fullList.Type != ItemType)
            {
                Debug.LogError($"Require {nameof(_fullList)} with type:{ItemType}");
                _fullList = null;
                return;
            }
            
            if (_defaultList is not null && _defaultCapacity < _defaultList.ItemDatas.Count)
            {
                _defaultCapacity = _defaultList.ItemDatas.Count;
            }
        }
    }
}