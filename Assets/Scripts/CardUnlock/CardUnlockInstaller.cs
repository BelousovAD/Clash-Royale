using System.Collections.Generic;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace CardUnlock
{
    internal class CardUnlockInstaller : MonoBehaviour, IInstaller
    {
        private const ItemType CardItemType = ItemType.Card;
        
        [SerializeField] private ItemDataList _fullCardList;
        
        private ContainerBuilder _builder;
        private CardUnlocker _cardUnlocker;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _cardUnlocker = new CardUnlocker(_fullCardList);

            _builder.AddSingleton(_cardUnlocker);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _cardUnlocker.Initialize(container.Resolve<IEnumerable<Container>>());
        }

        private void OnValidate()
        {
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