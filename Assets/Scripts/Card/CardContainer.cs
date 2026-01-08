using Item;

namespace Card
{
    public class CardContainer : Container
    {
        public CardContainer(ContainerData data)
            : base(data)
        { }

        protected override Item.Item CreateItem(ItemData data) =>
            new Card(data as CardData);
    }
}