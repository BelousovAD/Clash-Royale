using EnemyFind;
using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace UnitMovement
{
    public class UnitMover : MonoBehaviour
    {
        private const StateType ActivityState = StateType.Move;

        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;
        
        private float _radius;
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

        private void FixedUpdate()
        {
            if (_isActive && _enemy is not null)
            {
                _agent.SetDestination(_enemy.transform.position);
                _agent.isStopped = false;
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        public void UpdateRadius(float radius) =>
            _radius = radius;

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

        private void UpdateEnemy()
        {
            _enemy = _enemyFindCaller.Enemy;
            _agent.stoppingDistance = _radius + (_enemy?.Radius ?? 0f);
        }

        private void UpdateActivity() =>
            _isActive = _stateSwitcher?.CurrentState.Type == ActivityState;
    }
}