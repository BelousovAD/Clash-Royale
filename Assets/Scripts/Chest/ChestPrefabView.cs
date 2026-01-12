using Item;
using UnityEngine;

namespace Chest
{
    public class ChestPrefabView : ItemView
    {
        [SerializeField] private Transform _parent;

        private GameObject _prefabInstance;

        private new Chest Item => base.Item as Chest;
        
        public override void UpdateView()
        {
            Destroy(_prefabInstance);
            
            if (Item is not null)
            {
                _prefabInstance = Instantiate(Item.Prefab, Vector3.zero, Quaternion.identity, _parent);
            }
        }
    }
}