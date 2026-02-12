using Currency;
using Reflex.Core;
using UnityEngine;

namespace Elixir
{
    internal class ElixirInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField][Min(0)] private int _defaultValue = 5;
        [SerializeField][Min(0)] private int _maxValue = 10;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(new Currency.Currency(CurrencyType.Elixir, _defaultValue, _maxValue));
            builder.AddSingleton(new Currency.Currency(CurrencyType.EnemyElixir, _defaultValue, _maxValue));
        }
    }
}