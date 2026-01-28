using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardBattle
{
    internal class RandomItemSelector : IDisposable
    {
        private readonly ContainerType _type;
        private Container _container;
        
        public RandomItemSelector(ContainerType containerType) =>
            _type = containerType;

        public void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _type)
                {
                    _container = container;
                    break;
                }
            }
        }

        public void Dispose() =>
            _container.Deselect();

        public void Select(IEnumerable<string> excludeSubtypes = null)
        {
            List<Item.Item> items = new (_container.Items);
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