using Item;

namespace Card
{
    internal class CardContainer : Container
    {
        public CardContainer(ContainerData data)
            : base(data)
        { }

        public override Item.Item CreateItem(ItemData data, int id = DefaultId) =>
            new Card(data as CardData, id);
    }
}