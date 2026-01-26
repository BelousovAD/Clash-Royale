using System.Collections.Generic;
using Character;
using Item;
using Reflex.Core;
using UnityEngine;
using UnityEngine.UI;
using Container = Item.Container;

namespace CardBattle
{
    internal class CardBattleInstaller : MonoBehaviour, IInstaller
    {
        private const ItemType CardItemType = ItemType.Card;

        [SerializeField] private ItemDataList _fullCardList;
        [SerializeField] private Indicator _indicatorInstance;
        [SerializeField] private Camera _indicatorCamera;
        [SerializeField] private LayerMask _layerMask;
        

        private PointerIndicator _pointerIndicator;
        private RandomItemSelector _itemSelector;
        private SelectedItemToContainerCopier _itemCopier;
        private HandCardContainerFiller _containerFiller;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _pointerIndicator = new PointerIndicator(_layerMask, _indicatorInstance, _indicatorCamera);
            _itemSelector = new RandomItemSelector(ContainerType.EquippedCard);
            _itemCopier = new SelectedItemToContainerCopier(
                ContainerType.EquippedCard,
                ContainerType.HandCard,
                _fullCardList.ItemDatas);
            _containerFiller = new HandCardContainerFiller(_itemSelector, _itemCopier);

            _builder.AddSingleton(_itemSelector);
            _builder.AddSingleton(_itemCopier);
            _builder.AddSingleton(_containerFiller);
            _builder.AddSingleton(_pointerIndicator);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _itemSelector.Initialize(container.Resolve<IEnumerable<Container>>());
            _itemCopier.Initialize(container.Resolve<IEnumerable<Container>>());
            _containerFiller.Initialize(container.Resolve<IEnumerable<Container>>());
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