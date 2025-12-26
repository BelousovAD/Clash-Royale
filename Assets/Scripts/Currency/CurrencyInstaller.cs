using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Currency
{
    internal class CurrencyInstaller : MonoBehaviour, IInstaller
    {
        private Money _money;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _money = new Money();

            _builder.AddSingleton(_money);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _money.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}