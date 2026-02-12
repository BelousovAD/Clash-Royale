using Item;
using Rarity;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = nameof(CardData), menuName = nameof(Card) + "/" + nameof(CardData))]
    public class CardData : ItemData
    {
        [SerializeField] private RarityType _rarity;
        [SerializeField][Min(0)] private int _price;
        [SerializeField] private bool _isLocked;
        
        public RarityType Rarity => _rarity;

        public int Price => _price;

        public bool IsLocked => _isLocked;
    }
}