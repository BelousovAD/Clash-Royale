using Item;

namespace Card
{
    internal class CardContainer : Container
    {
        public CardContainer(ContainerData data)
            : base(data)
        { }

        protected override Item.Item CreateItem(ItemData data) =>
            new Card(data as CardData);
    }
}