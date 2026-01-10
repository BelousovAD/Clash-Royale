using Item;
using Rarity;
using UnityEngine;

namespace Card
{
    internal class CardRarityView : ItemImageView
    {
        [SerializeField] private RarityData _rarityData;

        private new Card Item => base.Item as Card;
        
        public override void UpdateView() =>
            Image.color = Item is null ? RarityData.DefaultColor : _rarityData.GetColor(Item.Rarity);
    }
}