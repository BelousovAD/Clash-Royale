using System.Collections.Generic;
using FSM;
using Input;

namespace Unit
{
    internal class UnitStateMachineBuilder : AbstractStateMachineBuilder
    {
        private readonly Unit _unit;
        private readonly IInputReader _inputReader;

        public UnitStateMachineBuilder(Unit unit, IInputReader inputReader)
        {
            _unit = unit;
            _inputReader = inputReader;
        }

        public override StateMachine Build()
        {
            StateMachine stateMachine = base.Build();
            stateMachine.SwitchStateTo(States[StateType.Idle]);

            return stateMachine;
        }

        protected override void BuildStates()
        {
            States = new Dictionary<StateType, State>
            {
                [StateType.Idle] = new (StateType.Idle),
                [StateType.Attack] = new (StateType.Attack),
                [StateType.Move] = new (StateType.Move),
                [StateType.Die] = new (StateType.Die),
            };
        }

        protected override void BuildTransitions()
        {
            States[StateType.Idle].AddTransitionRange(new []
            {
                new Transition(
                    _inputReader.MoveInput,
                    () => _inputReader.MoveInput.Value,
                    States[StateType.Move]),
                new Transition(
                    _inputReader.AttackInput,
                    () => _inputReader.AttackInput.Value,
                    States[StateType.Attack]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
            States[StateType.Attack].AddTransitionRange(new []
            {
                new Transition(
                    _inputReader.MoveInput,
                    () => _inputReader.MoveInput.Value,
                    States[StateType.Move]),
                new Transition(
                    _inputReader.AttackInput,
                    () => _inputReader.AttackInput.Value == false,
                    States[StateType.Idle]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
            States[StateType.Move].AddTransitionRange(new []
            {
                new Transition(
                    _inputReader.AttackInput,
                    () => _inputReader.AttackInput.Value,
                    States[StateType.Attack]),
                new Transition(
                    _inputReader.MoveInput,
                    () => _inputReader.MoveInput.Value == false,
                    States[StateType.Idle]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
        }
    }
}