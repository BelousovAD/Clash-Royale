using System.Collections.Generic;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace CardFilling
{
    internal class CardFillingInstaller : MonoBehaviour, IInstaller
    {
        private const ItemType CardItemType = ItemType.Card;
        private const ContainerType EquippedCardContainer = ContainerType.EquippedCard;
        private const ContainerType HandCardContainer = ContainerType.HandCard;
        private const ContainerType EnemyEquippedCardContainer = ContainerType.EnemyEquippedCard;
        private const ContainerType EnemyHandCardContainer = ContainerType.EnemyHandCard;

        [SerializeField] private ItemDataList _fullCardList;

        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            
            _builder.AddTransient(Create);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            Container equippedCardContainer = null;
            Container handCardContainer = null;
            Container enemyEquippedCardContainer = null;
            Container enemyHandCardContainer = null;

            foreach (Container itemContainer in container.Resolve<IEnumerable<Container>>())
            {
                switch (itemContainer.Type)
                {
                    case EquippedCardContainer:
                        equippedCardContainer = itemContainer;
                        break;
                    case HandCardContainer:
                        handCardContainer = itemContainer;
                        break;
                    case EnemyEquippedCardContainer:
                        enemyEquippedCardContainer = itemContainer;
                        break;
                    case EnemyHandCardContainer:
                        enemyHandCardContainer = itemContainer;
                        break;
                }
            }
            
            container.Resolve<ContainerFiller>().Initialize(equippedCardContainer, handCardContainer);
            container.Resolve<ContainerFiller>().Initialize(enemyEquippedCardContainer, enemyHandCardContainer);
        }

        private ContainerFiller Create(Reflex.Core.Container _) =>
            new (CardItemType, _fullCardList.ItemDatas);

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