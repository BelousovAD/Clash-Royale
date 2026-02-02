using System.Collections;
using FSM;
using UnityEngine;

namespace HealthChanging
{
    internal class HealthChanger : MonoBehaviour
    {
        private const StateType AttackState = StateType.Attack;

        [SerializeField] private Unit.Unit _unit;

        private float _amount;
        private Unit.Unit _target;
        private IStateSwitcher _stateSwitcher;
        private WaitForSeconds _wait;

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable() =>
            _unit.Initialized -= UpdateSubscriptions;

        public void SetAmount(float amount) =>
            _amount = amount;

        public void SetDelay(float delay) =>
            _wait = new WaitForSeconds(delay);

        public void SetEnemy(Unit.Unit enemy) =>
            _target = enemy;

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += ChangeHealth;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= ChangeHealth;

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

            ChangeHealth();
        }

        private void ChangeHealth()
        {
            if (_stateSwitcher is not null && _stateSwitcher.CurrentState.Type == AttackState)
            {
                StartCoroutine(ChangeHealthAfterDelay());
            }
        }

        private IEnumerator ChangeHealthAfterDelay()
        {
            yield return _wait;
            
            _target.Health.Take(_amount);
        }
    }
}