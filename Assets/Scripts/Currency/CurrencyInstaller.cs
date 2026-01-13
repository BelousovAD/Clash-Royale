using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Currency
{
    internal class CurrencyInstaller : MonoBehaviour, IInstaller
    {
        private Currency _money;
        private Currency _trophy;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _money = new Currency(CurrencyType.Money);
            _trophy = new Currency(CurrencyType.Trophy);

            _builder.AddSingleton(_money);
            _builder.AddSingleton(_trophy);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _trophy.Initialize(container.Resolve<SavvyServicesProvider>());
            _money.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}