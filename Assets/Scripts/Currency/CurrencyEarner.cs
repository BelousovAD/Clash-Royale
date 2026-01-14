using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Currency
{
    public class CurrencyEarner : MonoBehaviour
    {
        [SerializeField] private CurrencyType _currencyType = CurrencyType.Elixir;

        private Currency _elixir;

        [Inject]
        private void Initialize(IEnumerable<Currency> currencies)
        {
            foreach (Currency currency in currencies)
            {
                if (currency.Type == _currencyType)
                {
                    _elixir = currency;
                    break;
                }
            }
        }

        private void Start() =>
            _elixir.StartEarning();
    }
}