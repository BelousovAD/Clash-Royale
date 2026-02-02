using System.Collections.Generic;
using FSM;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay
{
    internal class CrownCatcher : MonoBehaviour
    {
        private const StateType DieState = StateType.Die;
        
        [SerializeField] private Unit.Unit _tower;
        [SerializeField] private CrownType _type;

        private CrownCounter _crownCounter;
        private IStateSwitcher _stateSwitcher;

        [Inject]
        private void Initialize(IEnumerable<CrownCounter> crownCounters)
        {
            foreach (CrownCounter crownCounter in crownCounters)
            {
                if (crownCounter.Type == _type)
                {
                    _crownCounter = crownCounter;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _tower.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _tower.Initialized -= UpdateSubscriptions;

            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }
        }

        private void CatchCrown()
        {
            if (_stateSwitcher?.CurrentState.Type == DieState)
            {
                _crownCounter.CountUp();
            }
        }

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += CatchCrown;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= CatchCrown;

        private void UpdateSubscriptions()
        {
            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }

            _stateSwitcher = _tower.StateSwitcher;

            if (_stateSwitcher is not null)
            {
                Subscribe();
            }

            CatchCrown();
        }
    }
}