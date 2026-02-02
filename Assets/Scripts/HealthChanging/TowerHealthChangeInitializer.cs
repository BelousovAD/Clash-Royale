using Character;
using EnemyFind;
using UnityEngine;

namespace HealthChanging
{
    internal class TowerHealthChangeInitializer : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;
        [SerializeField] private CharacterData _data;
        [SerializeField] private HealthChanger _healthChanger;

        private Unit.Unit _enemy;

        private void OnEnable()
        {
            _enemyFindCaller.EnemyFound += UpdateEnemy;
            UpdateEnemy();
            Initialize();
        }

        private void OnDisable() =>
            _enemyFindCaller.EnemyFound -= UpdateEnemy;

        private void UpdateEnemy()
        {
            _enemy = _enemyFindCaller.Enemy;
            _healthChanger.SetEnemy(_enemy);
        }

        private void Initialize()
        {
            _healthChanger.SetAmount(_data.HealthChangeAmount);
            _healthChanger.SetDelay(_data.HealthChangeDelay);
            _healthChanger.SetEnemy(_enemy);
        }
    }
}