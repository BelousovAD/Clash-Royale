using System.Collections.Generic;
using Character;
using Currency;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardDrop
{
    public class DropCardArea : MonoBehaviour
    {
        private const ContainerType HandCardContainerType = ContainerType.HandCard;

        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private CharacterProviderSpawnCaller _caller;

        private Container _container;
        private Currency.Currency _currency;

        [Inject]
        private void Initialize(IEnumerable<Container> containers, IEnumerable<Currency.Currency> currencies)
        {
            foreach (Container container in containers)
            {
                if (container.Type == HandCardContainerType)
                {
                    _container = container;
                    break;
                }
            }

            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == _currencyType)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        public void Receive()
        {
            Card.Card card = _container.Selected as Card.Card;

            if (_currency.Value >= card!.Price)
            {
                _currency.Spend(card.Price);
                _caller.CallSpawn(card.Subtype);
                _container.RemoveAt(_container.Index);
            }
            else
            {
                _container.Deselect();
            }
        }
    }
}