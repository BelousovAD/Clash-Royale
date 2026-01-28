using System.Collections.Generic;
using Item;
using Spawn;
using UnityEngine;

namespace Unit
{
    public class UnitSpawner : SingleItemProviderSpawner
    {
        private readonly List<Unit> _spawnedUnits = new ();

        public IReadOnlyList<Unit> SpawnedUnits => _spawnedUnits;
        
        protected override void Initialize(PooledComponent pooledComponent, Vector3 position)
        {
            base.Initialize(pooledComponent);
            pooledComponent.transform.position = position;
            _spawnedUnits.Add(pooledComponent.GetComponent<Unit>());
        }

        protected override void ReleaseAll()
        {
            _spawnedUnits.Clear();
            base.ReleaseAll();
        }

        protected override void Remove(PooledComponent pooledComponent)
        {
            _spawnedUnits.Remove(pooledComponent.GetComponent<Unit>());
            base.Remove(pooledComponent);
        }
    }
}