using Item;
using Savvy.Extensions;

namespace Card
{
    internal class CardPriceView : ItemTextView<Card>
    {
        protected override void UpdateView() =>
            TextField.text = string.Format(Format, Item is null ? string.Empty : Item.Price.ToNumsFormat());
    }
}