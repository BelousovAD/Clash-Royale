using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using FSM;
using Unit;
using UnitSpawn;
using UnityEngine;

namespace EnemyFind
{
    internal class ClosestUnitFinder
    {
        private const StateType DieState = StateType.Die;
        
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

        public Unit.Unit FindClosest(Unit.Unit from, Priority priority)
        {
            return priority switch
            {
                Priority.Tower => FindClosest(from, _towers.Concat(_spawner.SpawnedUnits)),
                Priority.TowerOnly => FindClosest(from, _towers),
                Priority.Unit => FindClosest(from, _spawner.SpawnedUnits) ?? FindClosest(from, _towers),
                _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null)
            };
        }

        private static Unit.Unit FindClosest(Unit.Unit from, IEnumerable<Unit.Unit> units)
        {
            float distance = float.MaxValue;
            Unit.Unit closest = null;
            
            foreach (Unit.Unit unit in units)
            {
                if (unit == from || unit.StateSwitcher.CurrentState.Type == DieState)
                {
                    continue;
                }
                
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