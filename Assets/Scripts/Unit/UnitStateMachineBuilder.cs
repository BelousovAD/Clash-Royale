using System.Collections.Generic;
using EnemyObserve;
using FSM;

namespace Unit
{
    internal class UnitStateMachineBuilder : AbstractStateMachineBuilder
    {
        private readonly Unit _unit;
        private readonly EnemyApproachObserver _enemyApproachObserver;

        public UnitStateMachineBuilder(Unit unit, EnemyApproachObserver enemyApproachObserver)
        {
            _unit = unit;
            _enemyApproachObserver = enemyApproachObserver;
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
                    _enemyApproachObserver.IsClose,
                    () => _enemyApproachObserver.IsClose.Value == false,
                    States[StateType.Move]),
                new Transition(
                    _enemyApproachObserver.IsClose,
                    () => _enemyApproachObserver.IsClose.Value == true,
                    States[StateType.Attack]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
            States[StateType.Attack].AddTransitionRange(new []
            {
                new Transition(
                    _enemyApproachObserver.IsClose,
                    () => _enemyApproachObserver.IsClose.Value == false,
                    States[StateType.Move]),
                new Transition(
                    _enemyApproachObserver.IsClose,
                    () => _enemyApproachObserver.IsClose.Value == null,
                    States[StateType.Idle]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
            States[StateType.Move].AddTransitionRange(new []
            {
                new Transition(
                    _enemyApproachObserver.IsClose,
                    () => _enemyApproachObserver.IsClose.Value == true,
                    States[StateType.Attack]),
                new Transition(
                    _enemyApproachObserver.IsClose,
                    () => _enemyApproachObserver.IsClose.Value == null,
                    States[StateType.Idle]),
                new Transition(
                    _unit.Health,
                    () => _unit.Health.IsDead,
                    States[StateType.Die]),
            });
        }
    }
}