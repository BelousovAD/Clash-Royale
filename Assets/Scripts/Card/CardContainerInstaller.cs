using Bootstrap;
using Item;
using Reflex.Core;
using UnityEngine;

namespace Card
{
    public class CardContainerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ContainerData _cardContainerData;
        [SerializeField] private ContainerData _equippedCardContainerData;
        
        private Item.Container _cardContainer;
        private Item.Container _equippedCardContainer;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _cardContainer = new CardContainer(_cardContainerData);
            _equippedCardContainer = new CardContainer(_equippedCardContainerData);

            _builder
                .AddSingleton(_cardContainer)
                .AddSingleton(_equippedCardContainer);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _cardContainer.Initialize(container.Resolve<SavvyServicesProvider>());
            _equippedCardContainer.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}