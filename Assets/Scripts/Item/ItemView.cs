using UnityEngine;

namespace Item
{
    public abstract class ItemView<T> : MonoBehaviour
        where T : Item
    {
        [SerializeField] private ItemProvider _itemProvider;

        protected T Item { get; private set; }

        protected virtual void OnEnable()
        {
            _itemProvider.Changed += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        protected virtual void OnDisable()
        {
            _itemProvider.Changed -= UpdateSubscriptions;

            if (Item is not null)
            {
                Unsubscribe();
            }
        }
        
        protected virtual void Subscribe()
        { }
        
        protected virtual void Unsubscribe()
        { }

        protected abstract void UpdateView();

        private void UpdateSubscriptions()
        {
            if (Item is not null)
            {
                Unsubscribe();
            }
            
            Item = _itemProvider.Item as T;

            if (Item is not null)
            {
                Subscribe();
            }
            
            UpdateView();
        }
    }
}