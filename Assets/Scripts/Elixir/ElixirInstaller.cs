using Currency;
using Reflex.Core;
using UnityEngine;

namespace Elixir
{
    internal class ElixirInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField][Min(0)] private int _defaultValue = 5;
        [SerializeField][Min(0)] private int _maxValue = 10;
        
        private Currency.Currency _currency;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _currency = new Currency.Currency(CurrencyType.Elixir, _defaultValue, _maxValue);

            builder.AddSingleton(_currency);
        }
    }
}