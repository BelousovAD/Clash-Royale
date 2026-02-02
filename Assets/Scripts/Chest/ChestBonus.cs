using System.Collections.Generic;
using Bootstrap;
using Common;
using Currency;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Chest
{
    public class ChestBonus : AbstractButton
    {
        private const CurrencyType MoneyCurrencyType = CurrencyType.Money;
        private const ContainerType ChestContainerType = ContainerType.Chest;

        [SerializeField] private int _timeRemove;

        private ChestBonusCalculator _bonusCalculator;
        private Currency.Currency _money;
        private SavvyServicesProvider _services;
        private Container _container;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies
            , IEnumerable<Container> containers
            , SavvyServicesProvider servicesProvider)
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

            _services = servicesProvider;
        }

        protected override void Awake()
        {
            base.Awake();
            _bonusCalculator = GetComponent<ChestBonusCalculator>();
        }

        protected override void HandleClick()
        {
            if (IsAnyChestUnlocking())
            {
                if (_bonusCalculator.IsMoneyEnough)
                {
                    SpendMoney(_bonusCalculator.BonusPrice);
                }
                else
                {
                    ShowAdv();
                }
            }
        }

        private void ShowAdv() =>
            _services.Mediation.ShowRewardedAd(() => ChangeChestTime());

        private void SpendMoney(int bonusPrice)
        {
            _money.Spend(bonusPrice);
            ChangeChestTime();
        }
        
        private bool IsAnyChestUnlocking()
        {
            foreach (Chest chest in _container.Items)
            {
                if (chest.IsUnlocking)
                {
                    return true;
                }
            }

            return false;
        }

        private void ChangeChestTime()
        {
            foreach (Chest chest in _container.Items)
            {
                if (chest.IsUnlocking)
                {
                    chest.Timer.Remove(_timeRemove);
                }
            }
        }
    }
}