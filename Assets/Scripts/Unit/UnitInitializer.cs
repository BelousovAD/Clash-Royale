using FSM;
using Item;
using UnityEngine;

namespace Unit
{
    internal class UnitInitializer : MonoBehaviour
    {
        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Unit _unit;
        
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
                _stateMachineBuilder = new UnitStateMachineBuilder(_unit, _unit.IsEnemyClose, character.AttackSpeed);
                _stateMachine = _stateMachineBuilder.Build();
                _unit.Initialize(_stateMachine);
            }
        }
    }
}