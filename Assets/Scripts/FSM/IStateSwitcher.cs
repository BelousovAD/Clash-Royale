using System;

namespace FSM
{
    public interface IStateSwitcher
    {
        public event Action StateSwitching;
        public event Action StateSwitched;
		
        public State CurrentState { get; }
		
        public void SwitchStateTo(State nextState);
    }
}