using System;
using System.Collections.Generic;
using System.Linq;
using Item;

namespace CardBattle
{
    internal class HandCardContainerFiller : IDisposable
    {
        private const ContainerType HandCardContainerType = ContainerType.HandCard;
        
        private readonly RandomItemSelector _itemSelector;
        private readonly SelectedItemToContainerCopier _itemCopier;
        private Container _handCardContainer;

        public HandCardContainerFiller(RandomItemSelector itemSelector, SelectedItemToContainerCopier itemCopier)
        {
            _itemSelector = itemSelector;
            _itemCopier = itemCopier;
        }

        public void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == HandCardContainerType)
                {
                    _handCardContainer = container;
                }
            }

            _handCardContainer.ContentChanged += FillUp;
            _itemSelector.Select(_handCardContainer.Items.Select(item => item.Subtype));
            FillUp();
        }

        public void Dispose() =>
            _handCardContainer.ContentChanged -= FillUp;

        private void FillUp()
        {
            _handCardContainer.ContentChanged -= FillUp;
            
            while (_handCardContainer.Items.Count < _handCardContainer.Capacity)
            {
                _itemCopier.Copy();
                _itemSelector.Select(_handCardContainer.Items.Select(item => item.Subtype));
            }
            
            _handCardContainer.ContentChanged += FillUp;
        }
    }
}