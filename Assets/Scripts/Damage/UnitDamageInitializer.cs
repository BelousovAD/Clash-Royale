using EnemyFind;
using Unit;
using UnityEngine;

namespace Damage
{
    public class UnitDamageInitializer : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;
        [SerializeField] private UnitPrefabView _unitPrefabView;
        [SerializeField] private Damager _damager;

        private float _damage;
        private float _delay;
        private Unit.Unit _enemy;

        private void OnEnable()
        {
            _enemyFindCaller.EnemyFound += UpdateEnemy;
            _unitPrefabView.InstanceChanged += Initialize;
            UpdateEnemy();
            Initialize();
        }

        private void OnDisable()
        {
            _enemyFindCaller.EnemyFound -= UpdateEnemy;
            _unitPrefabView.InstanceChanged -= Initialize;
        }

        public void UpdateDamage(float damage)
        {
            _damage = damage;
            _damager.SetDamage(_damage);
        }

        public void UpdateDamageDelay(float delay)
        {
            _delay = delay;
            _damager.SetDelay(_delay);
        }

        private void UpdateEnemy()
        {
            _enemy = _enemyFindCaller.Enemy;
            _damager.SetEnemy(_enemy);
        }

        private void Initialize()
        {
            if (_unitPrefabView.Instance is not null)
            {
                _damager.SetDamage(_damage);
                _damager.SetDelay(_delay);
                _damager.SetEnemy(_enemy);
            }
        }
    }
}