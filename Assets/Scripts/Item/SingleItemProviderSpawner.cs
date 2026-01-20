using System.Collections.Generic;
using Spawn;

namespace Item
{
    public class SingleItemProviderSpawner : SiblingsSpawner
    {
        private readonly List<PooledComponent> _spawnedItemProviders = new ();

        private void OnDisable() =>
            ReleaseAll();

        public void Spawn(Item item)
        {
            PooledComponent pooledComponent = Spawn();
            _spawnedItemProviders.Add(pooledComponent);
            ItemProvider itemProvider = pooledComponent.GetComponent<ItemProvider>();
            itemProvider.Initialize(item);

            InitializeProvider(itemProvider);
        }

        private void ReleaseAll()
        {
            _spawnedItemProviders.ForEach(itemProvider => itemProvider.Release());
            _spawnedItemProviders.Clear();
        }

        protected virtual void InitializeProvider(ItemProvider itemProvider)
        { }
    }
}