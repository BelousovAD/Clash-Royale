using System.Collections.Generic;
using System.Linq;
using Item;

namespace CardFilling
{
    internal static class SelectedItemToContainerCopier
    {
        public static void Copy(Container from, Container to, IEnumerable<ItemData> fullItemDatas)
        {
            if (from.Selected is null)
            {
                return;
            }

            ItemData data = fullItemDatas.First(data => data.Subtype == from.Selected.Subtype);
            to.Add(to.CreateItem(data));
        }
    }
}