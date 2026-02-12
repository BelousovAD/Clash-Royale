using System;
using System.Collections.Generic;
using System.Linq;
using Item;

namespace CardFilling
{
    internal class ContainerFiller : IDisposable
    {
        private readonly List<ItemData> _itemDatas;
        private Container _from;
        private Container _to;

        public ContainerFiller(IEnumerable<ItemData> fullItemDatas) =>
            _itemDatas = new List<ItemData>(fullItemDatas);

        public void Initialize(Container from, Container to)
        {
            if (_to is not null)
            {
                _to.ContentChanged -= FillUp;
            }
            
            _from = from;
            _to = to;
            
            _to.ContentChanged += FillUp;
            RandomItemSelector.Select(_from, _to.Items.Select(item => item.Subtype));
            FillUp();
        }

        public void Dispose()
        {
            if (_to is not null)
            {
                _to.ContentChanged -= FillUp;
            }
        }

        private void FillUp()
        {
            _to.ContentChanged -= FillUp;
            
            while (_to.Items.Count < _to.Capacity)
            {
                SelectedItemToContainerCopier.Copy(_from, _to, _itemDatas);
                RandomItemSelector.Select(_from, _to.Items.Select(item => item.Subtype));
            }
            
            _to.ContentChanged += FillUp;
        }
    }
}