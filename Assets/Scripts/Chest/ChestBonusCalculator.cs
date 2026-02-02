using System;
using System.Collections.Generic;
using Currency;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Chest
{
    internal class ChestBonusCalculator : MonoBehaviour
    {
        private const CurrencyType MoneyCurrencyType = CurrencyType.Money;
        private const ContainerType ChestContainerType = ContainerType.Chest;

        private Currency.Currency _money;
        private Container _container;

        public int BonusPrice { get; private set; }
        
        public bool IsMoneyEnough => _money.Value > BonusPrice;

        public event Action Calculated;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies, IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == ChestContainerType)
                {
                    _container = container;
                    break;
                }
            }

            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == MoneyCurrencyType)
                {
                    _money = currency;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _container.ContentChanged += SubscribeOnChest;
            SubscribeOnChest();
            _money.Changed += SetPrice;
            SetPrice();
        }

        private void OnDisable()
        {
            _container.ContentChanged -= SubscribeOnChest;
            _money.Changed -= SetPrice;
            UnsubscribeOnChest();
        }

        private void SetPrice()
        {
            int newPrice = 0;

            foreach (Chest chest in _container.Items)
            {
                if (chest.IsUnlocking)
                {
                    newPrice += chest.Price;
                    chest.UnlockingStatusChanged -= SetPrice;
                }
            }

            BonusPrice = newPrice;
            Calculated?.Invoke();
        }

        private void UnsubscribeOnChest()
        {
            foreach (Chest chest in _container.Items)
            {
                chest.UnlockingStatusChanged -= SetPrice;
                chest.Unlocked -= SetPrice;
            }
        }

        private void SubscribeOnChest()
        {
            foreach (Chest chest in _container.Items)
            {
                chest.UnlockingStatusChanged += SetPrice;
                chest.Unlocked += SetPrice;
            }
        }
    }
}