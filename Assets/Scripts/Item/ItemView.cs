using UnityEngine;

namespace Item
{
    internal abstract class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemProvider _itemProvider;

        protected Item Item => _itemProvider.Item;

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