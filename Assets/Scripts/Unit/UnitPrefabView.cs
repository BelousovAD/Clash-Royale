using System;
using Item;
using UnityEngine;

namespace Unit
{
    public class UnitPrefabView : ItemView<Character.Character>
    {
        [SerializeField] private Transform _parent;

        private GameObject _instance;

        public event Action InstanceChanged;

        public GameObject Instance
        {
            get => _instance;

            private set
            {
                if (value != _instance)
                {
                    _instance = value;
                    InstanceChanged?.Invoke();
                }
            }
        }

        protected override void UpdateView()
        {
            if (Item is not null)
            {
                Destroy(Instance);
                Instance = Instantiate(Item.Prefab, _parent);
                Instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
        }
    }
}