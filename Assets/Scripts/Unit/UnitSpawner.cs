using System.Collections.Generic;
using Item;
using RayPointer;
using Spawn;
using UnityEngine;

namespace Unit
{
    public class UnitSpawner : SingleItemProviderSpawner
    {
        private readonly List<Unit> _spawnedUnits = new ();

        [SerializeField] private UnitType _type;
        [SerializeField] private Indicator _indicator;

        public IReadOnlyList<Unit> SpawnedUnits => _spawnedUnits;

        protected override void Initialize(PooledComponent pooledComponent)
        {
            base.Initialize(pooledComponent);
            Unit unit = pooledComponent.GetComponent<Unit>();
            unit.transform.position = _indicator.PositionToSpawn;
            unit.SetType(_type);
            _spawnedUnits.Add(unit);
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