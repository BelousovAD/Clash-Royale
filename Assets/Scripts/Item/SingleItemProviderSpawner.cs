using System.Collections.Generic;
using Spawn;
using UnityEngine;

namespace Item
{
    public class SingleItemProviderSpawner : SiblingsSpawner
    {
        private readonly List<PooledComponent> _spawnedObjects = new ();

        private void OnEnable() =>
            ComponentReleased += Remove;

        private void OnDisable()
        {
            ComponentReleased -= Remove;
            ReleaseAll();
        }

        public void Spawn(Item item)
        {
            PooledComponent pooledComponent = Spawn();
            _spawnedObjects.Add(pooledComponent);
            ItemProvider itemProvider = pooledComponent.GetComponent<ItemProvider>();
            itemProvider.Initialize(item);
            Initialize(pooledComponent);
        }       
        
        protected virtual void Initialize(PooledComponent pooledComponent)
        { }     

        protected virtual void ReleaseAll()
        {
            _spawnedObjects.ForEach(spawnedObject => spawnedObject.Release());
            _spawnedObjects.Clear();
        }

        protected virtual void Remove(PooledComponent pooledComponent) =>
            _spawnedObjects.Remove(pooledComponent);
    }
}