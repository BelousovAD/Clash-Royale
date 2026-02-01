using Character;
using EnemyFind;
using UnityEngine;

namespace Damage
{
    public class TowerDamageInitializer : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;
        [SerializeField] private CharacterData _data;
        [SerializeField] private MeleeDamager _damager;

        private float _damage;
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
            _damager?.SetEnemy(_enemy);
        }

        private void Initialize()
        {
            _damager.SetDamage(_damage);
            _damager.SetEnemy(_enemy);
            _damager.Initialize(_unit);
        }
    }
}