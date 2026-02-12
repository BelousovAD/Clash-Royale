using System.Collections.Generic;
using Currency;

namespace Reward
{
    internal class CurrencyRewarder : Rewarder
    {
        private readonly CurrencyType _currencyType;
        private Currency.Currency _currency;

        public CurrencyRewarder(RewardData data, CurrencyType currencyType)
            : base(data) =>
            _currencyType = currencyType;

        public void Initialize(Gameplay.Judge judge, IEnumerable<Currency.Currency> currencies)
        {
            Initialize(judge);
            
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == _currencyType)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        protected override void ApplyPenalty() =>
            _currency.Spend(LoseAmount);

        protected override void ApplyReward() =>
            _currency.Earn(WinAmount);
    }
}