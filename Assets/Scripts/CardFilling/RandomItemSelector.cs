using System.Collections.Generic;
using Item;
using Random = UnityEngine.Random;

namespace CardFilling
{
    internal static class RandomItemSelector
    {
        public static void Select(Container container, IEnumerable<string> excludeSubtypes = null)
        {
            if (container.Items.Count == 0)
            {
                return;
            }
            
            List<Item.Item> items = new (container.Items);
            List<string> excludes = excludeSubtypes != null ? new List<string>(excludeSubtypes) : new List<string>();

            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (excludes.Contains(items[i].Subtype))
                {
                    items.RemoveAt(i);
                }
            }
            
            items[Random.Range(0, items.Count)].Select();
        }
    }
}