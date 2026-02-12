using Character;
using FSM;
using UnityEngine;

namespace Unit
{
    internal class TowerInitializer : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private CharacterData _data;
        [SerializeField] private UnitType _type;
        
        private TowerStateMachineBuilder _stateMachineBuilder;
        private StateMachine _stateMachine;

        private void OnEnable() =>
            Initialize();

        private void Initialize()
        {
            _unit.Initialize(_data.Health, _data.Radius);
            _unit.SetType(_type);
            _stateMachineBuilder = new TowerStateMachineBuilder(_unit, _unit.IsEnemyClose, _data.AttackSpeed);
            _stateMachine = _stateMachineBuilder.Build();
            _unit.Initialize(_stateMachine);
        }
    }
}