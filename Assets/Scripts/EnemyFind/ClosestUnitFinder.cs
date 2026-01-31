using System.Collections.Generic;
using Unit;
using UnitSpawn;
using UnityEngine;

namespace EnemyFind
{
    internal class ClosestUnitFinder
    {
        private UnitSpawner _spawner;
        private List<Unit.Unit> _towers;
        private Unit.Unit _closest;

        public ClosestUnitFinder(UnitType typeToFind) =>
            TypeToFind = typeToFind;

        public UnitType TypeToFind { get; }

        public void Initialize(UnitSpawner spawner, IEnumerable<Unit.Unit> towers)
        {
            _spawner = spawner;
            _towers = new List<Unit.Unit>(towers);
        }

        public Unit.Unit FindClosest(Unit.Unit from)
        {
            _closest = FindClosest(from, _spawner.SpawnedUnits);

            return _closest ?? FindClosest(from, _towers);
        }

        private static Unit.Unit FindClosest(Unit.Unit from, IEnumerable<Unit.Unit> units)
        {
            float distance = float.MaxValue;
            Unit.Unit closest = null;
            
            foreach (Unit.Unit unit in units)
            {
                float distanceTemporary = Vector3.Magnitude(unit.transform.position - from.transform.position)
                                          - unit.Radius;

                if (distanceTemporary < distance)
                {
                    distance = distanceTemporary;
                    closest = unit;
                }
            }

            return closest;
        }
    }
}