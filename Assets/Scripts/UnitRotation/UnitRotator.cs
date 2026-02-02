using EnemyFind;
using FSM;
using UnityEngine;

namespace UnitRotation
{
    public class UnitRotator : MonoBehaviour
    {
        private const StateType ActivityState = StateType.Attack;
        
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private Transform _transformToRotate;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;
        
        private Unit.Unit _enemy;
        private bool _isActive;
        private IStateSwitcher _stateSwitcher;

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            _enemyFindCaller.EnemyFound += UpdateEnemy;
            UpdateEnemy();
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _unit.Initialized -= UpdateSubscriptions;
            _enemyFindCaller.EnemyFound -= UpdateEnemy;

            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }
        }

        private void Update()
        {
            if (_isActive && _enemy is not null)
            {
                Vector3 lookDirection = Vector3.Normalize(_enemy.transform.position - _transformToRotate.position);
                _transformToRotate.forward = new Vector3(lookDirection.x, 0f, lookDirection.z);
            }
        }

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += UpdateActivity;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= UpdateActivity;

        private void UpdateSubscriptions()
        {
            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }
            
            _stateSwitcher = _unit.StateSwitcher;

            if (_stateSwitcher is not null)
            {
                Subscribe();
            }
            
            UpdateActivity();
        }

        private void UpdateActivity() =>
            _isActive = _stateSwitcher?.CurrentState.Type == ActivityState;

        private void UpdateEnemy() =>
            _enemy = _enemyFindCaller.Enemy;
    }
}