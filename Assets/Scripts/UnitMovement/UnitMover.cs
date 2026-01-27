using FSM;
using Item;
using UnityEngine;
using UnityEngine.AI;

namespace UnitMovement
{
    public class UnitMover : MonoBehaviour
    {
        private const StateType ActivityState = StateType.Move;

        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private NavMeshAgent _agent;
        
        private float _unitRadius;
        private Transform _enemy;
        private bool _isActive;
        private IStateSwitcher _unitStateSwitcher;

        private void OnEnable()
        {
            _itemProvider.Changed += UpdateRadius;
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _itemProvider.Changed -= UpdateRadius;
            _unit.Initialized -= UpdateSubscriptions;

            if (_unitStateSwitcher is not null)
            {
                Unsubscribe();
            }
        }

        private void FixedUpdate()
        {
            if (_isActive && _enemy is not null)
            {
                _agent.SetDestination(_enemy.position);
                _agent.isStopped = false;
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        public void SetEnemy(Transform enemy, float enemyRadius)
        {
            if (enemy != _enemy)
            {
                _enemy = enemy;
                _agent.stoppingDistance = _unitRadius + enemyRadius;
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

        private void UpdateRadius() =>
            _unitRadius = (_itemProvider.Item as Character.Character)?.Radius ?? 0f;
        
        private void UpdateActivity() =>
            _isActive = _unitStateSwitcher?.CurrentState.Type == ActivityState;
    }
}