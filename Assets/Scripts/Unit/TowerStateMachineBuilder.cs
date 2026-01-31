using System.Collections.Generic;
using ChangeableValue;
using FSM;

namespace Unit
{
    internal class TowerStateMachineBuilder : AbstractStateMachineBuilder
    {
        private readonly Unit _unit;
        private readonly ChangeableValue<bool?> _isEnemyClose;

        public TowerStateMachineBuilder(Unit unit, ChangeableValue<bool?> isEnemyClose)
        {
            _unit = unit;
            _isEnemyClose = isEnemyClose;
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
                [StateType.Die] = new (StateType.Die),
            };
        }

        protected override void BuildTransitions()
        {
            States[StateType.Idle].AddTransitionRange(new []
            {
                new Transition(
                    _isEnemyClose,
                    () => _isEnemyClose.Value == true,
                    States[StateType.Attack]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
            States[StateType.Attack].AddTransitionRange(new []
            {
                new Transition(
                    _isEnemyClose,
                    () => _isEnemyClose.Value == null,
                    States[StateType.Idle]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
        }
    }
}