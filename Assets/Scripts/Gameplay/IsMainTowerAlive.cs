using System;
using ChangeableValue;
using FSM;

namespace Gameplay
{
    internal class IsMainTowerAlive : ChangeableValue<bool>, IDisposable
    {
        private const StateType DieState = StateType.Die;

        private readonly Unit.Unit _tower;
        private IStateSwitcher _stateSwitcher;

        public IsMainTowerAlive(Unit.Unit tower)
        {
            _tower = tower;
            Value = true;
            _tower.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }
        
        public void Dispose()
        {
            _tower.Initialized -= UpdateSubscriptions;
            
            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }
        }

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += UpdateValue;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= UpdateValue;

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

            UpdateValue();
        }

        private void UpdateValue()
        {
            if (_stateSwitcher is not null)
            {
                Value = _stateSwitcher.CurrentState.Type != DieState;
            }
        }
    }
}