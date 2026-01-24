using System;
using Behaviour;

namespace FSM
{
    public class Transition : IDisposable
    {
        private readonly Func<bool> _condition;
        private readonly IChangeable _parameterToCheck;

        public Transition(IChangeable parameterToCheck, Func<bool> condition, State nextState)
        {
            _parameterToCheck = parameterToCheck;
            _condition = condition;
            NextState = nextState;
            _parameterToCheck.Changed += CheckCondition;
        }

        public event Action<Transition> ConditionMet;
        
        public State NextState { get; }

        public void Dispose() =>
            _parameterToCheck.Changed -= CheckCondition;

        public void CheckCondition()
        {
            if (_condition.Invoke())
            {
                ConditionMet?.Invoke(this);
            }
        }
    }
}