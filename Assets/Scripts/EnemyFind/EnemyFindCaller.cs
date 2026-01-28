using System.Collections.Generic;
using EnemyObserve;
using Reflex.Attributes;
using Unit;
using UnitMovement;
using UnitRotation;
using UnityEngine;

namespace EnemyFind
{
    internal class EnemyFindCaller : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _root;
        [SerializeField] private EnemyApproachObserver _enemyApproachObserver;
        [SerializeField] private UnitMover _mover;
        [SerializeField] private UnitRotator _rotator;

        private List<ClosestUnitFinder> _unitFinders;
        private ClosestUnitFinder _unitFinder;

        [Inject]
        private void Initialize(IEnumerable<ClosestUnitFinder> closestUnitFinders) =>
            _unitFinders = new List<ClosestUnitFinder>(closestUnitFinders);

        public void Initialize(UnitType typeToFind)
        {
            foreach (ClosestUnitFinder unitFinder in _unitFinders)
            {
                if (unitFinder.TypeToFind == typeToFind)
                {
                    _unitFinder = unitFinder;
                    break;
                }
            }
        }
        
        private void Update()
        {
            if (_unitFinder is null)
            {
                return;
            }
            
            Unit.Unit closest = _unitFinder.FindClosest(_root);
            
            if (closest is not null)
            {
                _enemyApproachObserver.SetEnemy(closest.transform, closest.Radius);
                _mover.SetEnemy(closest.transform, closest.Radius);
                _rotator.SetEnemy(closest.transform);
            }
            else
            {
                _enemyApproachObserver.SetEnemy(null, Unit.Unit.MinRadius);
                _mover.SetEnemy(null, Unit.Unit.MinRadius);
                _rotator.SetEnemy(null);
            }
        }
    }
}