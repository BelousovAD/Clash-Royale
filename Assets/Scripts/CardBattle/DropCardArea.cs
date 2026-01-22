using System.Collections.Generic;
using Currency;
using Item;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CardBattle
{
    [RequireComponent(typeof(Image))]
    internal class DropCardArea : MonoBehaviour
    {
        private const ContainerType HandCardContainerType = ContainerType.HandCard;
        
        [SerializeField] private CurrencyType _currencyType;
        
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
                _container.RemoveAt(_container.Index);
            }
            else
            {
                _container.Deselect();
            }
        }
    }
}