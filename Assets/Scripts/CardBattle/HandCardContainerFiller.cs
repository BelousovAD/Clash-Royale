using System;
using System.Collections.Generic;
using System.Linq;
using Item;

namespace CardBattle
{
    internal class HandCardContainerFiller : IDisposable
    {
        private const ContainerType HandCardContainerType = ContainerType.HandCard;
        private const ContainerType EquippedCardContainerType = ContainerType.EquippedCard;
        
        private readonly RandomItemSelector _itemSelector;
        private readonly SelectedItemToContainerCopier _itemCopier;
        private Container _handCardContainer;
        private Container _equippedCardContainer;

        public HandCardContainerFiller(RandomItemSelector itemSelector, SelectedItemToContainerCopier itemCopier)
        {
            _itemSelector = itemSelector;
            _itemCopier = itemCopier;
        }

        public void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                switch (container.Type)
                {
                    case HandCardContainerType:
                        _handCardContainer = container;
                        break;
                    case EquippedCardContainerType:
                        _equippedCardContainer = container;
                        break;
                }
            }

            _equippedCardContainer.SelectChanged += FillUp;
            _itemSelector.Select(_handCardContainer.Items.Select(item => item.Subtype));
        }

        public void Dispose() =>
            _equippedCardContainer.SelectChanged -= FillUp;

        private void FillUp()
        {
            if (_handCardContainer.Items.Count < _handCardContainer.Capacity)
            {
                _itemCopier.Copy();
                _itemSelector.Select(_handCardContainer.Items.Select(item => item.Subtype));
            }
        }
    }
}