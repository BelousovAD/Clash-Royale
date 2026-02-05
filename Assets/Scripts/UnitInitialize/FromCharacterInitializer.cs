using EnemyFind;
using EnemyObserve;
using HealthChanging;
using Item;
using UnitAnimation;
using UnitMovement;
using UnityEngine;
using UnityEngine.AI;

namespace UnitInitialize
{
    internal class FromCharacterInitializer : MonoBehaviour
    {
        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private UnitHealthChangeInitializer _healthChangeInitializer;
        [SerializeField] private EnemyFindCaller _findCaller;
        [SerializeField] private EnemyApproachObserver _approachObserver;
        [SerializeField] private UnitAnimationInitializer _animationInitializer;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private UnitMover _mover;

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
                _healthChangeInitializer.UpdateHealthChangeAmount(character.HealthChangeAmount);
                _healthChangeInitializer.UpdateDamageDelay(character.HealthChangeDelay);
                _findCaller.UpdatePriority(character.Priority);
                _findCaller.UpdateSubtype(character.Subtype);
                _approachObserver.Initialize(character.AttackRange);
                _animationInitializer.UpdateAttackSpeed(character.AttackSpeed);
                _agent.radius = character.Radius;
                _agent.stoppingDistance = character.AttackRange;
                _agent.speed = character.MoveSpeed;
                _mover.UpdateRadius(character.Radius);
            }
        }
    }
}