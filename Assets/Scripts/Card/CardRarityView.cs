using Item;
using Rarity;
using UnityEngine;

namespace Card
{
    internal class CardRarityView : ItemImageView<Card>
    {
        [SerializeField] private RarityData _rarityData;

        protected override void UpdateView() =>
            Image.color = Item is null ? RarityData.DefaultColor : _rarityData.GetColor(Item.Rarity);
    }
}