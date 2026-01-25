using EnemyObserve;
using FSM;
using Item;
using UnityEngine;

namespace Unit
{
    internal class UnitInitializer : ItemView<Character.Character>
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private UnitReleaser _releaser;
        [SerializeField] private EnemyApproachObserver _enemyApproachObserver;
        [SerializeField][Min(0f)] private float _deathAnimationDuration;
        
        private UnitStateMachineBuilder _stateMachineBuilder;
        private StateMachine _stateMachine;

        protected override void UpdateView()
        {
            if (Item is not null)
            {
                _unit.Initialize(Item.Health);
                _stateMachineBuilder = new UnitStateMachineBuilder(_unit, _enemyApproachObserver);
                _stateMachine = _stateMachineBuilder.Build();
                _unit.Initialize(_stateMachine);
                _releaser.Initialize(_deathAnimationDuration);
            }
        }
    }
}