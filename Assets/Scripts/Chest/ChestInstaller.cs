using System.Collections.Generic;
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
        private const ItemType CardItemType = ItemType.Card;
        
        [SerializeField] private ContainerData _chestContainerData;
        [SerializeField] private ItemDataList _fullCardList;
        
        private ContainerBuilder _builder;
        private Container _chestContainer;
        private CardUnlocker _cardUnlocker;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _chestContainer = new ChestContainer(_chestContainerData);
            _cardUnlocker = new CardUnlocker(_fullCardList);

            _builder.AddSingleton(_chestContainer, typeof(Container));
            _builder.AddSingleton(_cardUnlocker);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _chestContainer.Initialize(container.Resolve<SavvyServicesProvider>());
            _cardUnlocker.Initialize(container.Resolve<IEnumerable<Container>>());
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

            if (_fullCardList is not null && _fullCardList.Type != CardItemType)
            {
                Debug.LogError(
                    $"Require {nameof(_fullCardList)} with " +
                    $"{nameof(_fullCardList.Type)}:{CardItemType}");
                _fullCardList = null;
            }
        }
    }
}