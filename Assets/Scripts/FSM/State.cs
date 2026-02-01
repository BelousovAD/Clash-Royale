using System;
using System.Collections.Generic;
using Behaviour;
using UnityEngine;

namespace FSM
{
    public class State : IEnterable, IExitable, IDisposable, IUpdatable
    {
        private const float MinBusyTime = 0f;
        
        private readonly List<Transition> _transitions = new ();
        private readonly float _busyTime;
        private IStateSwitcher _stateSwitcher;
        private bool _isBusy;
        private float _countdown;

        public State(StateType type, float busyTime = MinBusyTime)
        {
            Type = type;
            _busyTime = busyTime;
        }

        private event Action BusynessChanged;

        public StateType Type { get; }

        private bool IsBusy
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
            IsBusy = true;
            _countdown = _busyTime;
            SubscribeToTransitions();
            BusynessChanged += CheckTransitions;
        }

        public virtual void Exit()
        {
            BusynessChanged -= CheckTransitions;
            UnsubscribeFromTransitions();
        }

        public virtual void Update(float deltaTime)
        {
            if (_countdown > MinBusyTime)
            {
                _countdown -= deltaTime;
            }
            
            if (_countdown <= MinBusyTime)
            {
                IsBusy = false;
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