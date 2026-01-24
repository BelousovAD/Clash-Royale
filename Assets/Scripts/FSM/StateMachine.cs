using System;
using System.Collections.Generic;
using Behaviour;

namespace FSM
{
    public class StateMachine : IStateSwitcher, IFixedUpdatable, ILateUpdatable, IUpdatable, IDisposable
    {
        private readonly List<State> _states;

        public StateMachine(IEnumerable<State> states) =>
            _states = new List<State>(states);

        public event Action StateSwitching;
        public event Action StateSwitched;

        public State CurrentState { get; private set; }

        public void Update(float deltaTime)
        {
            if (CurrentState is IUpdatable updatableState)
            {
                updatableState.Update(deltaTime);
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            if (CurrentState is IFixedUpdatable fixedUpdatableState)
            {
                fixedUpdatableState.FixedUpdate(deltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            if (CurrentState is ILateUpdatable lateUpdatableState)
            {
                lateUpdatableState.LateUpdate(deltaTime);
            }
        }

        public void SwitchStateTo(State nextState)
        {
            if (_states.Contains(nextState) == false)
            {
                throw new ArgumentOutOfRangeException(
                    nextState.GetType().Name,
                    $"State {nextState.GetType().Name} doesnt exist in StateMachine");
            }

            if (nextState == CurrentState)
            {
                return;
            }

            CurrentState?.Exit();
            StateSwitching?.Invoke();
            CurrentState = nextState;
            StateSwitched?.Invoke();
            CurrentState?.Enter();
        }

        public void Dispose()
        {
            CurrentState?.Exit();
            _states.ForEach(state => state.Dispose());
        }
    }
}