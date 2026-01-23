using FSM;
using Item;
using Inputs;
using UnityEngine;

namespace Character
{
    internal class CharacterInitializer : ItemView<Character>
    {
        [SerializeField] private Unit _unit;
        
        private IInputReader _inputReader;
        private UnitStateMachineBuilder _characterStateMachineBuilder;
        private StateMachine _stateMachine;

        protected override void UpdateView()
        {
            _characterStateMachineBuilder = new UnitStateMachineBuilder(_unit, _inputReader);
            _stateMachine = _characterStateMachineBuilder.Build();
            _unit.Initialize(_stateMachine);
        }
    }
}