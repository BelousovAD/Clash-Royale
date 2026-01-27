using Item;

namespace Chest
{
    internal class ChestContainer : SaveableContainer
    {
        public ChestContainer(ContainerData data)
            : base(data)
        { }

        public override Item.Item CreateItem(ItemData data, int id = DefaultId) =>
            new Chest(data as ChestData, id);
    }
}