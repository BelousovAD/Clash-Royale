using FSM;
using UnityEngine;

namespace UnitRotation
{
    public class UnitRotator : MonoBehaviour
    {
        private const StateType ActivityState = StateType.Attack;
        
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private Transform _transformToRotate;
        
        private Transform _enemy;
        private bool _isActive;
        private IStateSwitcher _unitStateSwitcher;

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _unit.Initialized -= UpdateSubscriptions;

            if (_unitStateSwitcher is not null)
            {
                Unsubscribe();
            }
        }

        private void Update()
        {
            if (_isActive && _enemy is not null)
            {
                _transformToRotate.LookAt(_enemy);
            }
        }

        public void SetEnemy(Transform enemy) =>
            _enemy = enemy;

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
    }
}