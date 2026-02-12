using EnemyFind;
using Unit;
using UnityEngine;

namespace EnemyObserve
{
    public class EnemyApproachObserver : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private Transform _transformFrom;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;

        private IsTransformClose _isClose;

        public void Initialize(float closeDistance) =>
            _isClose.Initialize(closeDistance);

        private void Awake()
        {
            _isClose = _unit.IsEnemyClose;
            _isClose.Initialize(_transformFrom);
        }

        private void OnEnable()
        {
            _enemyFindCaller.EnemyFound += UpdateEnemy;
            _isClose.Enable();
            UpdateEnemy();
        }

        private void OnDisable()
        {
            _isClose.Disable();
            _enemyFindCaller.EnemyFound -= UpdateEnemy;
        }

        private void Update() =>
            _isClose.Update(Time.deltaTime);

        private void UpdateEnemy()
        {
            Unit.Unit enemy = _enemyFindCaller.Enemy;
            
            if (enemy is null)
            {
                _isClose.SetTarget(null, 0f);
            }
            else
            {
                _isClose.SetTarget(enemy.transform, enemy.Radius);
            }
        }
    }
}