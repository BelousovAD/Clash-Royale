using FSM;
using UnityEngine;

namespace Damage
{
    internal class Damager : MonoBehaviour
    {
        private const StateType AttackState = StateType.Attack;
        
        [SerializeField] private UnitTrigger _unitTrigger;

        private float _damage;
        private Unit.Unit _unit;
        private Unit.Unit _enemy;

        public void Initialize(Unit.Unit unit)
        {
            if (_unit is not null)
            {
                Unsubscribe();
            }

            _unit = unit;

            if (_unit is not null)
            {
                Subscribe();
            }

            ChangeTriggerActivity();
        }

        public void SetDamage(float damage) =>
            _damage = damage;

        public void SetEnemy(Unit.Unit enemy) =>
            _enemy = enemy;

        private void Subscribe()
        {
            _unitTrigger.Changed += Damage;
            _unit.StateSwitcher.StateSwitched += ChangeTriggerActivity;
        }

        private void Unsubscribe()
        {
            _unitTrigger.Changed -= Damage;
            _unit.StateSwitcher.StateSwitched -= ChangeTriggerActivity;
        }

        private void ChangeTriggerActivity() =>
            _unitTrigger.enabled = _unit.StateSwitcher.CurrentState.Type is AttackState;

        private void Damage()
        {
            Unit.Unit detectedEnemy = _unitTrigger.Value;

            if (detectedEnemy is not null && detectedEnemy != _unit && detectedEnemy == _enemy)
            {
                detectedEnemy.Health.TakeDamage(_damage);
                _unitTrigger.enabled = false;
            }
        }
    }
}