using System;
using UnityEngine;

namespace Item
{
    internal abstract class ItemView<T> : MonoBehaviour
        where T : Enum
    {
        [SerializeField] private ItemProvider<T> _itemProvider;

        protected Item<T> Item => _itemProvider.Item;

        protected virtual void OnEnable()
        {
            _itemProvider.Changed += UpdateView;
            UpdateView();
        }

        protected virtual void OnDisable() =>
            _itemProvider.Changed -= UpdateView;

        public abstract void UpdateView();
    }
}