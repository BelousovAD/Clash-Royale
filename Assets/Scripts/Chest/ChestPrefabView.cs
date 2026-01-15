using Item;
using UnityEngine;

namespace Chest
{
    internal class ChestPrefabView : ItemView
    {
        [SerializeField] private Transform _parent;

        private GameObject _prefabInstance;

        private new Chest Item => base.Item as Chest;
        
        public override void UpdateView()
        {
            if (Item is null)
            {
                return;
            }
            
            Destroy(_prefabInstance);
            _prefabInstance = Instantiate(Item.Prefab, _parent);
            _prefabInstance.transform.localPosition = Vector3.zero;
            _prefabInstance.transform.localRotation = Quaternion.identity;
        }
    }
}