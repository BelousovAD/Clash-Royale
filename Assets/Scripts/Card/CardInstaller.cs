using System.Collections.Generic;
using Bootstrap;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace Card
{
    internal class CardInstaller : MonoBehaviour, IInstaller
    {
        private const ContainerType CardContainerType = ContainerType.Card;
        
        [SerializeField] private ContainerData _cardContainerData;
        [SerializeField] private ContainerData _equippedCardContainerData;
        [SerializeField] private ContainerData _handCardContainerData;

        private List<Container> _cardContainers;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _cardContainers = new List<Container>
            {
                new SaveableCardContainer(_cardContainerData),
                new SaveableCardContainer(_equippedCardContainerData),
                new CardContainer(_handCardContainerData),
            };

            _cardContainers.ForEach(cardContainer => _builder.AddSingleton(cardContainer, typeof(Container)));

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _cardContainers.ForEach(cardContainer =>
                cardContainer.Initialize(container.Resolve<SavvyServicesProvider>()));
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            ValidateField(ref _cardContainerData);
            ValidateField(ref _equippedCardContainerData);
            ValidateField(ref _handCardContainerData);
        }

        private static void ValidateField(ref ContainerData containerDataField)
        {
            if (containerDataField is not null && containerDataField.Type != CardContainerType)
            {
                Debug.LogError(
                    $"Require {nameof(containerDataField)} with " +
                    $"{nameof(containerDataField.Type)}:{CardContainerType}");
                containerDataField = null;
            }
        }
#endif
    }
}