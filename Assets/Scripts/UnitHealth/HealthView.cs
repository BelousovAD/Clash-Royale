using Unit;
using UnityEngine;

namespace UnitHealth
{
    internal abstract class HealthView : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;

        protected Health Health;
        
        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _unit.Initialized -= UpdateSubscriptions;
            Unsubscribe();
        }

        protected abstract void UpdateView();

        private void Subscribe() =>
            Health.Changed += UpdateView;

        private void Unsubscribe() =>
            Health.Changed -= UpdateView;

        private void UpdateSubscriptions()
        {
            if (Health is not null)
            {
                Unsubscribe();
            }

            Health = _unit.Health;

            if (Health is not null)
            {
                Subscribe();
            }

            UpdateView();
        }
    }
}