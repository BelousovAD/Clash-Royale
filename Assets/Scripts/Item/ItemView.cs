using UnityEngine;

namespace Item
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemProvider _itemProvider;

        protected Item Item => _itemProvider.Item;

        protected virtual void OnEnable()
        {
            _itemProvider.Changed += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        protected virtual void OnDisable() =>
            _itemProvider.Changed -= UpdateSubscriptions;

        public abstract void UpdateView();

        protected virtual void UpdateSubscriptions() =>
            UpdateView();
    }
}