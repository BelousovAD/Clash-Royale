using Bootstrap;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace Chest
{
    internal class ChestContainerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ContainerData _chestContainerData;
        
        private Container _chestContainer;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _chestContainer = new ChestContainer(_chestContainerData);

            _builder.AddSingleton(_chestContainer, typeof(Container));

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _chestContainer.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}