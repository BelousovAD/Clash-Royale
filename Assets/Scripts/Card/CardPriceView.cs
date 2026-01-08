using Item;
using Savvy.Extensions;

namespace Card
{
    internal class CardPriceView : ItemTextView
    {
        private new Card Item => base.Item as Card;
        
        public override void UpdateView() =>
            TextField.text = string.Format(Format, Item is null ? string.Empty : Item.Price.ToNumsFormat());
    }
}