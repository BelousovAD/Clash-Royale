using System;
using Unit;
using UnityEngine;

namespace EnemyFind
{
    internal class EnemyFindCallerInitializer : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;

        private void OnEnable()
        {
            _unit.TypeChanged += Initialize;
            Initialize();
        }

        private void OnDisable() =>
            _unit.TypeChanged -= Initialize;

        private void Initialize()
        {
            switch (_unit.Type)
            {
                case UnitType.Enemy:
                    _enemyFindCaller.Initialize(UnitType.Player);
                    break;
                case UnitType.Player:
                    _enemyFindCaller.Initialize(UnitType.Enemy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}