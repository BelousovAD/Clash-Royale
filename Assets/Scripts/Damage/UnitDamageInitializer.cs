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

        private float _damage;
        private Unit.Unit _enemy;
        private Damager _damager;

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
            _damager?.SetDamage(_damage);
        }

        private void UpdateEnemy()
        {
            _enemy = _enemyFindCaller.Enemy;
            _damager?.SetEnemy(_enemy);
        }

        private void Initialize()
        {
            if (_unitPrefabView.Instance is not null)
            {
                _damager = _unitPrefabView.Instance.GetComponent<Damager>();
                _damager.SetDamage(_damage);
                _damager.SetEnemy(_enemy);
                _damager.Initialize(_unit);
            }
        }
    }
}