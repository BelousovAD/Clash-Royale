using System;
using System.Collections.Generic;
using System.Linq;
using Currency;
using Item;

namespace ArtificialOpponent
{
    internal class CardChooser : IDisposable
    {
        private const ContainerType EnemyHandCardContainer = ContainerType.EnemyHandCard;
        private const CurrencyType EnemyElixirCurrency = CurrencyType.EnemyElixir;
        
        private Container _container;
        private Currency.Currency _currency;

        public void Initialize(IEnumerable<Container> containers, IEnumerable<Currency.Currency> currencies)
        {
            foreach (Container container in containers)
            {
                if (container.Type == EnemyHandCardContainer)
                {
                    _container = container;
                    break;
                }
            }

            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == EnemyElixirCurrency)
                {
                    _currency = currency;
                    break;
                }
            }

            _currency.Changed += ChooseCard;
        }

        public void Dispose() =>
            _currency.Changed -= ChooseCard;

        private void ChooseCard()
        {
            Card.Card cardToSelect = null;
            
            foreach (Card.Card card in _container.Items.Select(item => item as Card.Card))
            {
                if (card!.Price <= _currency.Value)
                {
                    cardToSelect = card;
                    break;
                }
            }
            
            cardToSelect?.Select();
        }
    }
}