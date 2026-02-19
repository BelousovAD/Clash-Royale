using Item;
using UnityEngine;

namespace Chest
{
    internal class ChestPrefabView : ItemView<Chest>
    {
        [SerializeField] private Transform _parent;

        private GameObject _prefabInstance;

        protected override void UpdateView()
        {
            if (Item is null)
            {
                return;
            }

            Destroy(_prefabInstance);
            _prefabInstance = Instantiate(Item.Prefab, _parent);
            _prefabInstance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
    }
}