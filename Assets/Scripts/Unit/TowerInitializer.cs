using Character;
using EnemyObserve;
using FSM;
using UnityEngine;

namespace Unit
{
    internal class TowerInitializer : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private EnemyApproachObserver _enemyApproachObserver;
        [SerializeField] private UnitAnimationCaller _animationCaller;
        [SerializeField] private UnitAnimator _animator;
        [SerializeField] private CharacterData _data;
        
        private TowerStateMachineBuilder _stateMachineBuilder;
        private StateMachine _stateMachine;

        private void OnEnable() =>
            Initialize();

        private void Initialize()
        {
            _unit.Initialize(_data.Health, _data.Radius);
            _enemyApproachObserver.Initialize(_data.AttackRange);
            _animationCaller.Initialize(_data.AttackSpeed);
            _stateMachineBuilder = new TowerStateMachineBuilder(_unit, _enemyApproachObserver);
            _stateMachine = _stateMachineBuilder.Build();
            _unit.Initialize(_stateMachine);
            _animationCaller.Initialize(_animator);
        }
    }
}