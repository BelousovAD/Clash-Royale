using Bootstrap;
using Currency;
using Reflex.Core;
using UnityEngine;

namespace Elixir
{
    public class ElixirInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private float _elixirTimeToEarn = 2;
        [SerializeField] private int _elixirValueToEarn = 1;
        [SerializeField] private int _defaultValue = 5;
        [SerializeField] private int _maxValue = 10;
        
        private Elixir _elixir;
        private ContainerBuilder _builder;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _elixir = new Elixir(CurrencyType.Elixir, _defaultValue, _maxValue, _elixirTimeToEarn, _elixirValueToEarn);

            _builder.AddSingleton(_elixir, typeof(Currency.Currency));
            
            _builder.OnContainerBuilt += Initialize;
        }
        
        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _elixir.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}