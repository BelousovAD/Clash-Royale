using System.Collections.Generic;
using Currency;
using Reflex.Attributes;
using UnityEngine;

namespace Elixir
{
    internal class ElixirEarnStarter : MonoBehaviour
    {
        private const CurrencyType CurrencyType = Currency.CurrencyType.Elixir;

        private Elixir _elixir;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == CurrencyType)
                {
                    _elixir = currency as Elixir;
                    break;
                }
            }
        }

        private void Start() =>
            _elixir.StartEarning();
    }
}