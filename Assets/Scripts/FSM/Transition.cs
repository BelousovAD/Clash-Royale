using System;
using Common;

namespace FSM
{
    public class Transition : IDisposable
    {
        private readonly Func<bool> _condition;
        private readonly IChangeableValue _parameterToCheck;

        public Transition(IChangeableValue parameterToCheck, Func<bool> condition, AbstractState nextState)
        {
            _parameterToCheck = parameterToCheck;
            _condition = condition;
            NextState = nextState;
            _parameterToCheck.Changed += CheckCondition;
        }

        public event Action<Transition> ConditionMet;
        
        public AbstractState NextState { get; }

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