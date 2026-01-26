using System.Collections.Generic;
using EnemyObserve;
using Reflex.Attributes;
using UnityEngine;

namespace EnemyFind
{
    internal class EnemyFindCaller : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _root;
        [SerializeField] private UnitType _typeToFind;
        [SerializeField] private EnemyApproachObserver _enemyApproachObserver;
        
        private ClosestUnitFinder _unitFinder;

        [Inject]
        private void Initialize(IEnumerable<ClosestUnitFinder> closestUnitFinders)
        {
            foreach (ClosestUnitFinder unitFinder in closestUnitFinders)
            {
                if (unitFinder.TypeToFind == _typeToFind)
                {
                    _unitFinder = unitFinder;
                    break;
                }
            }
        }

        private void Update()
        {
            Unit.Unit closest = _unitFinder.FindClosest(_root);
            
            if (closest is not null)
            {
                _enemyApproachObserver.SetEnemy(closest.transform, closest.Radius);
            }
            else
            {
                _enemyApproachObserver.SetEnemy(null, 0f);
            }
        }
    }
}