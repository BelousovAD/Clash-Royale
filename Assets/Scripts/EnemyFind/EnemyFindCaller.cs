using System;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace EnemyFind
{
    public class EnemyFindCaller : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _root;

        private List<ClosestUnitFinder> _unitFinders;
        private ClosestUnitFinder _unitFinder;
        private Unit.Unit _enemy;

        public event Action EnemyFound;

        public Unit.Unit Enemy
        {
            get
            {
                return _enemy;
            }

            private set
            {
                if (value != _enemy)
                {
                    _enemy = value;
                    EnemyFound?.Invoke();
                }
            }
        }

        [Inject]
        private void Initialize(IEnumerable<ClosestUnitFinder> closestUnitFinders) =>
            _unitFinders = new List<ClosestUnitFinder>(closestUnitFinders);

        private void OnEnable()
        {
            _root.TypeChanged += UpdateTypeToFind;
            UpdateTypeToFind();
        }

        private void OnDisable() =>
            _root.TypeChanged -= UpdateTypeToFind;

        private void Update()
        {
            if (_unitFinder is null)
            {
                return;
            }
            
            Enemy = _unitFinder.FindClosest(_root);
        }

        private void UpdateTypeToFind()
        {
            foreach (ClosestUnitFinder unitFinder in _unitFinders)
            {
                if (unitFinder.TypeToFind != _root.Type)
                {
                    _unitFinder = unitFinder;
                    break;
                }
            }
        }
    }
}