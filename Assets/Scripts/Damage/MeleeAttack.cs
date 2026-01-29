using FSM;
using UnityEngine;

namespace Damage
{
    internal class MeleeAttack : MonoBehaviour
    {
        private const StateType AttackState = StateType.Attack;
        
        [SerializeField, Min(0f)] private float _damage;
        [SerializeField] private AttackTrigger _attackTrigger;
        
         private Unit.Unit _character;
        
        private void OnEnable()
        {
            _character = GetComponentInParent<Unit.Unit>();
            _character.Initialized += Subscribe;
            Subscribe();
        }

        private void OnDisable() =>
            Unsubscribe();

        private void Subscribe()
        {
            if (_character.StateSwitcher is not null)
            {
                _character.Initialized -= Subscribe;
                _attackTrigger.Changed += Damage;
                _character.StateSwitcher.StateSwitched += ChangeTriggerActivity;
                ChangeTriggerActivity();
            }
        }

        private void Unsubscribe()
        {
            _character.Initialized -= Subscribe;

            if (_character.StateSwitcher is not null)
            {
                _character.StateSwitcher.StateSwitched -= ChangeTriggerActivity;
                _attackTrigger.Changed -= Damage;
            }
        }

        private void ChangeTriggerActivity() =>
            _attackTrigger.enabled = _character.StateSwitcher.CurrentState.Type is AttackState;

        private void Damage()
        {
            _attackTrigger.Value?.Health.TakeDamage(_damage);
            _attackTrigger.enabled = false;
        }
    }
}