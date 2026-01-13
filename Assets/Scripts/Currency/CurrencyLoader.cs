using System.Collections.Generic;
using Bootstrap;
using Reflex.Attributes;
using UnityEngine;

namespace Currency
{
    internal class CurrencyLoader : MonoBehaviour, ILoadable
    {
        private List<Currency> _currencies;

        [Inject]
        private void Initialize(IEnumerable<Currency> currencies) =>
            _currencies = new List<Currency>(currencies);

        public void Load() =>
            _currencies.ForEach(currency => currency.Load());
    }
}