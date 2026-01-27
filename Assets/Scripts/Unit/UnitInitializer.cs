using EnemyObserve;
using FSM;
using Item;
using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    internal class UnitInitializer : MonoBehaviour
    {
        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Unit _unit;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private UnitReleaser _releaser;
        [SerializeField] private EnemyApproachObserver _enemyApproachObserver;
        [SerializeField] private UnitAnimationCaller _animationCaller;
        [SerializeField][Min(0f)] private float _deathAnimationDuration;
        
        private UnitStateMachineBuilder _stateMachineBuilder;
        private StateMachine _stateMachine;

        private void OnEnable()
        {
            _itemProvider.Changed += Initialize;
            Initialize();
        }

        private void OnDisable() =>
            _itemProvider.Changed -= Initialize;

        private void Initialize()
        {
            if (_itemProvider.Item is Character.Character character)
            {
                _unit.Initialize(character.Health, character.Radius);
                _agent.radius = character.Radius;
                _agent.stoppingDistance = character.AttackRange;
                _agent.speed = character.MoveSpeed;
                _enemyApproachObserver.Initialize(character.AttackRange);
                _animationCaller.Initialize(character.AttackSpeed);
                _stateMachineBuilder = new UnitStateMachineBuilder(_unit, _enemyApproachObserver);
                _stateMachine = _stateMachineBuilder.Build();
                _unit.Initialize(_stateMachine);
                _releaser.Initialize(_deathAnimationDuration);
            }
        }
    }
}