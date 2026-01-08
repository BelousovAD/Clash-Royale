namespace Item
{
    internal class ItemIconView : ItemImageView
    {
        public override void UpdateView() =>
            Image.sprite = Item is null ? DefaultSprite : Item.Icon;
    }
}