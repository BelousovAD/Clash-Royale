using EnemyFind;
using Unit;
using UnityEngine;

namespace HealthChanging
{
    public class UnitHealthChangeInitializer : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;
        [SerializeField] private UnitPrefabView _unitPrefabView;
        [SerializeField] private HealthChanger _healthChanger;

        private float _amount;
        private float _delay;
        private Unit.Unit _enemy;

        private void OnEnable()
        {
            _unitPrefabView.InstanceChanged += Initialize;
            _enemyFindCaller.EnemyFound += UpdateEnemy;
            UpdateEnemy();
            Initialize();
        }

        private void OnDisable()
        {
            _enemyFindCaller.EnemyFound -= UpdateEnemy;
            _unitPrefabView.InstanceChanged -= Initialize;
        }

        public void UpdateHealthChangeAmount(float amount)
        {
            _amount = amount;
            _healthChanger.SetAmount(_amount);
        }

        public void UpdateDamageDelay(float delay)
        {
            _delay = delay;
            _healthChanger.SetDelay(_delay);
        }

        private void UpdateEnemy()
        {
            _enemy = _enemyFindCaller.Enemy;
            _healthChanger.SetEnemy(_enemy);
        }

        private void Initialize()
        {
            if (_unitPrefabView.Instance is not null)
            {
                _healthChanger.SetAmount(_amount);
                _healthChanger.SetDelay(_delay);
                _healthChanger.SetEnemy(_enemy);
            }
        }
    }
}