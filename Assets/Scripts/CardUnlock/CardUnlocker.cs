using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using Item;
using Rarity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardUnlock
{
    internal class CardUnlocker
    {
        private const int MinRandomValue = 0;
        
        private readonly ItemDataList _fullCardList;
        private Container _cardContainer;
        private Container _equippedCardContainer;
        private Container _chestContainer;

        public CardUnlocker(ItemDataList fullCardList) =>
            _fullCardList = fullCardList;

        public event Action<Card.Card> CardUnlocked;
        
        public void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                switch (container.Type)
                {
                    case ContainerType.EquippedCard:
                        _equippedCardContainer = container;
                        break;
                    case ContainerType.Card:
                        _cardContainer = container;
                        break;
                    case ContainerType.Chest:
                        _chestContainer = container;
                        break;
                    default:
                        continue;
                }
            }
        }

        public void UnlockCard()
        {
            if (_chestContainer.Selected is null)
            {
                Debug.LogError($"Can not get card rarity. Selected chest is null");
            }
            
            RarityType cardRarity = (_chestContainer.Selected as Chest.Chest)!.GetRandomRarity();
            Card.Card card = GetRandomCard(cardRarity);
            card.Unlock();
            CardUnlocked?.Invoke(card);
        }
        
        private Card.Card GetRandomCard(RarityType rarity)
        {
            IReadOnlyList<ItemData> cardDatas = _fullCardList.ItemDatas
                .Where(data => (data as CardData)!.Rarity == rarity)
                .ToList();
            ItemData cardData = cardDatas[Random.Range(MinRandomValue, cardDatas.Count)];
            IReadOnlyList<Item.Item> cards = _cardContainer.Items.Concat(_equippedCardContainer.Items).ToList();
            Card.Card card = cards
                .Select(item => item as Card.Card)
                .First(item => item!.Type == cardData.Type && item!.Subtype == cardData.Subtype);
            
            return card;
        }
    }
}