using System;

namespace Item
{
    internal abstract class SelectedItemProvider<T> : ItemProvider<T>
        where T : Enum
    {
        private ItemContainer<T> _container;

        public void Initialize(ItemContainer<T> container) =>
            _container = container;

        private void OnEnable()
        {
            _container.SelectChanged += UpdateItem;
            UpdateItem();
        }

        private void OnDisable() =>
            _container.SelectChanged -= UpdateItem;

        private void UpdateItem() =>
            Initialize(_container.Selected);
    }
}