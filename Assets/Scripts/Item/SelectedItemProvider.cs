namespace Item
{
    internal abstract class SelectedItemProvider : ItemProvider
    {
        private ItemContainer _container;

        public void Initialize(ItemContainer container) =>
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