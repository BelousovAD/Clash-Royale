using System.Collections.Generic;
using ChangeableValue;
using FSM;

namespace Unit
{
    internal class TowerStateMachineBuilder : AbstractStateMachineBuilder
    {
        private readonly Unit _unit;
        private readonly ChangeableValue<bool?> _isEnemyClose;
        private readonly float _attackSpeed;

        public TowerStateMachineBuilder(Unit unit,
            ChangeableValue<bool?> isEnemyClose,
            float attackSpeed)
        {
            _unit = unit;
            _isEnemyClose = isEnemyClose;
            _attackSpeed = attackSpeed;
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
                [StateType.Attack] = new (StateType.Attack, 1f / _attackSpeed),
                [StateType.Die] = new (StateType.Die),
            };
        }

        protected override void BuildTransitions()
        {
            States[StateType.Idle].AddTransitionRange(new []
            {
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
                new Transition(
                    _isEnemyClose,
                    () => _isEnemyClose.Value == true,
                    States[StateType.Attack]),
            });
            States[StateType.Attack].AddTransitionRange(new []
            {
                new Transition(
                    null,
                    () => true,
                    States[StateType.Idle]),
            });
        }
    }
}