using System.Collections.Generic;
using Reflex.Attributes;
using Spawn;
using UnityEngine;

namespace Item
{
    public class ItemProviderSpawner : SiblingsSpawner
    {
        [SerializeField] private ContainerType _containerType;
        
        private readonly List<PooledComponent> _spawnedItemProviders = new();
        private Container _container;
        
        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _containerType)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _container.ContentChanged += Respawn;
            Respawn();
        }

        private void OnDisable()
        {
            _container.ContentChanged -= Respawn;
            ReleaseAll();
        }

        private void ReleaseAll()
        {
            _spawnedItemProviders.ForEach(itemProvider => itemProvider.Release());
            _spawnedItemProviders.Clear();
        }

        private void Respawn()
        {
            ReleaseAll();
            
            foreach (Item item in _container.Items)
            {
                InitializeProvider(Spawn(item));
            }
        }

        private ItemProvider Spawn(Item item)
        {
            PooledComponent pooledComponent = Spawn();
            _spawnedItemProviders.Add(pooledComponent);
            ItemProvider itemProvider = pooledComponent.GetComponent<ItemProvider>();
            itemProvider.Initialize(item);

            return itemProvider;
        }

        protected virtual void InitializeProvider(ItemProvider itemProvider)
        { }
    }
}