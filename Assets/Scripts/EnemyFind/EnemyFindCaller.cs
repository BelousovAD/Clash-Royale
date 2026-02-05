using System;
using System.Collections.Generic;
using Character;
using Reflex.Attributes;
using UnityEngine;

namespace EnemyFind
{
    public class EnemyFindCaller : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private List<string> _excludes;
        
        private List<ClosestUnitFinder> _unitFinders;
        private ClosestUnitFinder _unitFinder;
        private Unit.Unit _enemy;
        private string _subtype;
        private Priority _priority;

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
            _unit.TypeChanged += UpdateTypeToFind;
            UpdateTypeToFind();
        }

        private void OnDisable() =>
            _unit.TypeChanged -= UpdateTypeToFind;

        private void Update()
        {
            if (_unitFinder is null)
            {
                return;
            }
            
            Enemy = _unitFinder.FindClosest(_unit, _priority);
        }

        public void UpdateSubtype(string subtype)
        {
            _subtype = subtype;
            UpdateTypeToFind();
        }

        public void UpdatePriority(Priority priority) =>
            _priority = priority;

        private void UpdateTypeToFind()
        {
            bool isExcludeSubtype = _excludes.Contains(_subtype);
            
            foreach (ClosestUnitFinder unitFinder in _unitFinders)
            {
                if ((isExcludeSubtype ^ unitFinder.TypeToFind == _unit.Type) == false)
                {
                    _unitFinder = unitFinder;
                    break;
                }
            }
        }
    }
}