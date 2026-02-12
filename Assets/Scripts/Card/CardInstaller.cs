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
        private const ItemType CardType = ItemType.Card;

        [SerializeField] private ContainerData _allCardContainerData;
        [SerializeField] private ContainerData _cardContainerData;
        [SerializeField] private ContainerData _equippedCardContainerData;

        private List<Container> _cardContainers;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _cardContainers = new List<Container>
            {
                new CardContainer(_allCardContainerData),
                new SaveableCardContainer(_cardContainerData),
                new SaveableCardContainer(_equippedCardContainerData),
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
            ValidateField(ref _allCardContainerData);
            ValidateField(ref _cardContainerData);
            ValidateField(ref _equippedCardContainerData);
        }

        private static void ValidateField(ref ContainerData containerDataField)
        {
            if (containerDataField is not null && containerDataField.ItemType != CardType)
            {
                Debug.LogError(
                    $"Require {nameof(containerDataField)} with " +
                    $"{nameof(containerDataField.ItemType)}:{CardType}");
                containerDataField = null;
            }
        }
#endif
    }
}