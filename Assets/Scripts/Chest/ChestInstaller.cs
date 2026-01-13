using Bootstrap;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace Chest
{
    internal class ChestInstaller : MonoBehaviour, IInstaller
    {
        private const ContainerType ChestContainerType = ContainerType.Chest;
        
        [SerializeField] private ContainerData _chestContainerData;
        
        private ContainerBuilder _builder;
        private Container _chestContainer;

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

        private void OnValidate()
        {
            if (_chestContainerData is not null && _chestContainerData.Type != ChestContainerType)
            {
                Debug.LogError(
                    $"Require {nameof(_chestContainerData)} with " +
                    $"{nameof(_chestContainerData.Type)}:{ChestContainerType}");
                _chestContainerData = null;
            }
        }
    }
}