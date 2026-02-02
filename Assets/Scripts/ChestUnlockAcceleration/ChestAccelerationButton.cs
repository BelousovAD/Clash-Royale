using System.Collections.Generic;
using Bootstrap;
using Common;
using Currency;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace ChestUnlockAcceleration
{
    public class ChestAccelerationButton : AbstractButton
    {
        private const CurrencyType MoneyCurrency = CurrencyType.Money;
        private const ContainerType ChestContainer = ContainerType.Chest;

        [SerializeField] private ChestAccelerationPriceCalculator _priceCalculator;
        [SerializeField] private int _timeToRemove;

        private Currency.Currency _currency;
        private Container _container;
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies,
            IEnumerable<Container> containers,
            SavvyServicesProvider servicesProvider)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == MoneyCurrency)
                {
                    _currency = currency;
                    break;
                }
            }
            
            foreach (Container container in containers)
            {
                if (container.Type == ChestContainer)
                {
                    _container = container;
                    break;
                }
            }
            
            _services = servicesProvider;
        }

        protected override void HandleClick()
        {
            if (_currency.Value >= _priceCalculator.Price)
            {
                _currency.Spend(_priceCalculator.Price);
                ChangeChestTime();
            }
            else
            {
                _services.Mediation.ShowRewardedAd(ChangeChestTime);
            }
        }

        private void ChangeChestTime()
        {
            foreach (Item.Item item in _container.Items)
            {
                Chest.Chest chest = item as Chest.Chest;
                
                if (chest!.IsUnlocking)
                {
                    chest.Timer.Remove(_timeToRemove);
                }
            }
        }
    }
}