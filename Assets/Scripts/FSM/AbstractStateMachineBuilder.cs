using System;
using System.Collections.Generic;

namespace FSM
{
    public abstract class AbstractStateMachineBuilder
    {
        protected Dictionary<Type, AbstractState> States;
        private StateMachine _stateMachine;

        public virtual StateMachine Build()
        {
            BuildStates();
            BuildTransitions();
            _stateMachine = new StateMachine(States.Values);
            InitializeStates();

            return _stateMachine;
        }

        protected abstract void BuildStates();

        protected abstract void BuildTransitions();

        protected virtual void InitializeStates()
        {
            foreach (AbstractState state in States.Values)
            {
                state.SetStateSwitcher(_stateMachine);
            }
        }
    }
}