using Bootstrap;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace Character
{
    internal class CharacterContainerInstaller : MonoBehaviour, IInstaller
    {
        private const ContainerType CharacterContainerType = ContainerType.Character;
        
        [SerializeField] private ContainerData _characterContainerData;
        
        private ContainerBuilder _builder;
        private Container _characterContainer;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _characterContainer = new CharacterContainer(_characterContainerData);

            _builder.AddSingleton(_characterContainer, typeof(Container));
            
            _builder.OnContainerBuilt += Initialize;
        }
        
        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _characterContainer.Initialize(container.Resolve<SavvyServicesProvider>());
        }
        
        private void OnValidate()
        {
            if (_characterContainerData is not null && _characterContainerData.Type != CharacterContainerType)
            {
                Debug.LogError(
                    $"Require {nameof(_characterContainerData)} with " +
                    $"{nameof(_characterContainerData.Type)}:{CharacterContainerType}");
                _characterContainerData = null;
            }
        }
    }
}