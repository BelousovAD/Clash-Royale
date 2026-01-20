using Item;

namespace Chest
{
    internal class ChestContainer : SaveableContainer
    {
        public ChestContainer(ContainerData data)
            : base(data)
        { }

        protected override Item.Item CreateItem(ItemData data, int id) =>
            new Chest(data as ChestData, id);
    }
}