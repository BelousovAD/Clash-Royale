using System;

namespace FSM
{
    public interface IStateSwitcher
    {
        public event Action StateSwitching;
        public event Action StateSwitched;
		
        public AbstractState CurrentState { get; }
		
        public void SwitchStateTo(AbstractState nextState);
    }
}