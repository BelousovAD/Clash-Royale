using System.Collections.Generic;
using System.Linq;
using Item;

namespace CardBattle
{
    internal class SelectedItemToContainerCopier
    {
        private readonly ContainerType _fromContainerType;
        private readonly ContainerType _toContainerType;
        private readonly List<ItemData> _itemDatas;
        private Container _from;
        private Container _to;
        
        public SelectedItemToContainerCopier(
            ContainerType fromContainerType,
            ContainerType toContainerType,
            IEnumerable<ItemData> fullItemDatas)
        {
            _fromContainerType = fromContainerType;
            _toContainerType = toContainerType;
            _itemDatas = new List<ItemData>(fullItemDatas);
        }

        public void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _fromContainerType)
                {
                    _from = container;
                }
                else if (container.Type == _toContainerType)
                {
                    _to = container;
                }
            }
        }

        public void Copy()
        {
            if (_from.Selected is null)
            {
                return;
            }

            ItemData data = _itemDatas.First(data => data.Subtype == _from.Selected.Subtype);
            _to.Add(_to.CreateItem(data));
        }
    }
}