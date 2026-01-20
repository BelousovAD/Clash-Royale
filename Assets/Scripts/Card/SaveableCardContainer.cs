using Item;

namespace Card
{
    internal class SaveableCardContainer : SaveableContainer
    {
        public SaveableCardContainer(ContainerData data)
            : base(data)
        { }

        protected override Item.Item CreateItem(ItemData data, int id) =>
            new Card(data as CardData, id);
    }
}