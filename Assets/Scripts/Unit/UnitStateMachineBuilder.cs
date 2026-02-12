using System.Collections.Generic;
using FSM;

namespace Unit
{
    internal class UnitStateMachineBuilder : AbstractStateMachineBuilder
    {
        private const float AttackSpeedDenominatorMultiplier = 2f;
        private const float AttackSpeedBaseShift = 5f;
        private const float AttackSpeedNumerator = 25f;
        
        private readonly Unit _unit;
        private readonly ChangeableValue.ChangeableValue<bool?> _isEnemyClose;
        private readonly float _attackSpeed;

        public UnitStateMachineBuilder(Unit unit,
            ChangeableValue.ChangeableValue<bool?> isEnemyClose,
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
                [StateType.Attack] = new (StateType.Attack,((AttackSpeedNumerator - _attackSpeed) 
                                                            / (AttackSpeedDenominatorMultiplier * 
                                                               (_attackSpeed + AttackSpeedBaseShift)))),
                [StateType.Move] = new (StateType.Move),
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
                    () => _isEnemyClose.Value == false,
                    States[StateType.Move]),
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
            States[StateType.Move].AddTransitionRange(new []
            {
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
                new Transition(
                    _isEnemyClose,
                    () => _isEnemyClose.Value == true,
                    States[StateType.Attack]),
                new Transition(
                    _isEnemyClose,
                    () => _isEnemyClose.Value == null,
                    States[StateType.Idle]),
            });
        }
    }
}