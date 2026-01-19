namespace Item
{
    internal class ItemIconView : ItemImageView<Item>
    {
        protected override void UpdateView() =>
            Image.sprite = Item is null ? DefaultSprite : Item.Icon;
    }
}