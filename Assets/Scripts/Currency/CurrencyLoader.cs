using System.Collections.Generic;
using System.Linq;
using Bootstrap;
using Reflex.Attributes;
using UnityEngine;

namespace Currency
{
    internal class CurrencyLoader : MonoBehaviour, ILoadable
    {
        private List<SavebleCurrency> _currencies;

        [Inject]
        private void Initialize(IEnumerable<Currency> currencies) =>
            _currencies = new List<SavebleCurrency>(
                currencies.Select(currency => currency as SavebleCurrency)
                .Where(currency => currency != null));

        public void Load() =>
            _currencies.ForEach(currency => currency.Load());
    }
}