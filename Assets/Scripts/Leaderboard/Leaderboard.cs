using System;
using System.Collections.Generic;
using Currency;
using MirraGames.SDK;

namespace Leaderboard
{
    internal class Leaderboard : IDisposable
    {
        private const CurrencyType TrophyCurrency = CurrencyType.Trophy;

        private readonly string _id;
        private Currency.Currency _currency;

        public Leaderboard(string id) =>
            _id = id;

        public void Initialize(IEnumerable<Currency.Currency> currencies)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == TrophyCurrency)
                {
                    _currency = currency;
                    break;
                }
            }
            
            _currency.Changed += SaveScore;
        }

        public void Dispose() =>
            _currency.Changed -= SaveScore;

        private void SaveScore() =>
            MirraSDK.Achievements.SetScore(_id, _currency.Value);
    }
}