using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Currency
{
    internal class CurrencyInstaller : MonoBehaviour, IInstaller
    {
        private SavebleCurrency _money;
        private SavebleCurrency _trophy;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _money = new SavebleCurrency(CurrencyType.Money);
            _trophy = new SavebleCurrency(CurrencyType.Trophy);

            _builder.AddSingleton(_money, typeof(Currency));
            _builder.AddSingleton(_trophy, typeof(Currency));

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