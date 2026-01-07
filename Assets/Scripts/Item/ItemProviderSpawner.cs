using System.Collections.Generic;
using Spawn;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    internal class ItemProviderSpawner : SiblingsSpawner
    {
        [SerializeField] private ToggleGroup _toggleGroup;

        private readonly List<PooledComponent> _spawnedItemProviders = new();

        private void OnDestroy() =>
            ReleaseAll();

        protected void Spawn(Item item)
        {
            PooledComponent itemProvider = Spawn();
            itemProvider.GetComponent<Toggle>().group = _toggleGroup;
            itemProvider.GetComponent<ItemProvider>().Initialize(item);
            _spawnedItemProviders.Add(itemProvider);
        }

        protected void ReleaseAll()
        {
            _spawnedItemProviders.ForEach(itemProvider => itemProvider.Release());
            _spawnedItemProviders.Clear();
        }
    }
}