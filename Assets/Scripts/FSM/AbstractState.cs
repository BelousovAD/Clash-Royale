using System;
using System.Collections.Generic;
using Common;

namespace FSM
{
    public abstract class AbstractState : IEnterable, IExitable, IDisposable
    {
        private readonly List<Transition> _transitions = new ();
        private IStateSwitcher _stateSwitcher;
        private bool _isBusy;
        private bool _isFirstUpdate;

        private event Action BusynessChanged;

        protected bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    BusynessChanged?.Invoke();
                }
            }
        }

        public void AddTransition(Transition transition) =>
            _transitions.Add(transition);

        public void AddTransitionRange(IEnumerable<Transition> transitions) =>
            _transitions.AddRange(transitions);

        public virtual void Enter()
        {
            SubscribeToTransitions();
            BusynessChanged += CheckTransitions;
            _isFirstUpdate = true;
        }

        public virtual void Exit()
        {
            _isFirstUpdate = true;
            BusynessChanged -= CheckTransitions;
            UnsubscribeFromTransitions();
        }

        public virtual void Update(float deltaTime)
        {
            if (_isFirstUpdate)
            {
                CheckTransitions();
                _isFirstUpdate = false;
            }
        }

        public void Dispose()
        {
            BusynessChanged -= CheckTransitions;

            _transitions.ForEach(transition =>
            {
                transition.ConditionMet -= SwitchState;
                transition.Dispose();
            });
        }

        public void SetStateSwitcher(IStateSwitcher stateSwitcher) =>
            _stateSwitcher = stateSwitcher;

        private void SubscribeToTransitions() =>
            _transitions.ForEach(transition => transition.ConditionMet += SwitchState);

        private void UnsubscribeFromTransitions() =>
            _transitions.ForEach(transition => transition.ConditionMet -= SwitchState);

        private void CheckTransitions()
        {
            if (IsBusy == false)
            {
                _transitions.ForEach(transition => transition.CheckCondition());
            }
        }

        private void SwitchState(Transition transition)
        {
            if (IsBusy == false)
            {
                _stateSwitcher.SwitchStateTo(transition.NextState);
            }
        }
    }
}