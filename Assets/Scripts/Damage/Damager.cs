using System.Collections;
using FSM;
using UnityEngine;

namespace Damage
{
    internal class Damager : MonoBehaviour
    {
        private const StateType AttackState = StateType.Attack;

        [SerializeField] private Unit.Unit _unit;

        private float _damage;
        private Unit.Unit _enemy;
        private IStateSwitcher _stateSwitcher;
        private WaitForSeconds _wait;

        private void OnEnable()
        {
            _unit.Initialized += Initialize;
            Initialize();
        }

        private void OnDisable() =>
            _unit.Initialized -= Initialize;

        public void Initialize()
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

            Damage();
        }

        public void SetDamage(float damage) =>
            _damage = damage;

        public void SetDelay(float delay) =>
            _wait = new WaitForSeconds(delay);

        public void SetEnemy(Unit.Unit enemy) =>
            _enemy = enemy;

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += Damage;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= Damage;
        
        private void Damage()
        {
            if (_stateSwitcher is not null && _stateSwitcher.CurrentState.Type == AttackState)
            {
                StartCoroutine(DamageAfterDelay());
            }
        }

        private IEnumerator DamageAfterDelay()
        {
            yield return _wait;
            
            _enemy.Health.TakeDamage(_damage);
        }
    }
}