using System;
using System.Collections.Generic;
using FSM;
using Inputs;

namespace Character
{
    internal class UnitStateMachineBuilder : AbstractStateMachineBuilder
    {
        private readonly Unit _unit;
        private readonly IInputReader _inputReader;
        
        public UnitStateMachineBuilder(Unit unit , IInputReader inputReader)
        {
            _unit = unit;
            _inputReader = inputReader;
        }

        public override StateMachine Build()
        {
            StateMachine stateMachine = base.Build();
            stateMachine.SwitchStateTo(States[typeof(IdleState)]);

            return stateMachine;
        }

        protected override void BuildStates()
        {
            States = new Dictionary<Type, AbstractState>
            {
                [typeof(IdleState)] = new IdleState(),
                [typeof(RunningState)] = new RunningState(),
                [typeof(AttackState)] = new AttackState(_unit, _inputReader),
                [typeof(DyingState)] = new DyingState(),
            };
        }

        protected override void BuildTransitions()
        {
            States[typeof(IdleState)].AddTransitionRange(new[]
            {
                new Transition(, States[typeof(RunningState)]),
                new Transition(, States[typeof(AttackState)]),
                new Transition(, States[typeof(DyingState)]),
            });
            States[typeof(AttackState)].AddTransitionRange(new[]
            {
                new Transition(, States[typeof(IdleState)]),
                new Transition(, States[typeof(RunningState)]),
                new Transition(, States[typeof(DyingState)]),
            });
            States[typeof(RunningState)].AddTransitionRange(new[]
            {
                new Transition(, States[typeof(IdleState)]),
                new Transition(, States[typeof(AttackState)]),
                new Transition(, States[typeof(DyingState)]),
            });
        }
    }
}