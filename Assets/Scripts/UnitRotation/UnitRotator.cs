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
        private IStateSwitcher _unitStateSwitcher;

        private void OnEnable()
        {
            _enemyFindCaller.EnemyFound += UpdateEnemy;
            _unit.Initialized += UpdateSubscriptions;
            UpdateEnemy();
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _unit.Initialized -= UpdateSubscriptions;
            _enemyFindCaller.EnemyFound -= UpdateEnemy;

            if (_unitStateSwitcher is not null)
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
            _unitStateSwitcher.StateSwitched += UpdateActivity;

        private void Unsubscribe() =>
            _unitStateSwitcher.StateSwitched -= UpdateActivity;

        private void UpdateSubscriptions()
        {
            if (_unitStateSwitcher is not null)
            {
                Unsubscribe();
            }
            
            _unitStateSwitcher = _unit.StateSwitcher;

            if (_unitStateSwitcher is not null)
            {
                Subscribe();
            }
            
            UpdateActivity();
        }

        private void UpdateActivity() =>
            _isActive = _unitStateSwitcher?.CurrentState.Type == ActivityState;

        private void UpdateEnemy() =>
            _enemy = _enemyFindCaller.Enemy;
    }
}