using ChangeableValue;
using UnityEngine;

namespace Input
{
    public class UnitInputReader : MonoBehaviour, IInputReader
    {
        [SerializeField] private EnemyApproachObserver _enemyApproachObserver;
        
        public ChangeableValue<bool> AttackInput { get; private set; }
        public ChangeableValue<bool> MoveInput { get; private set; }

        private void Awake()
        {
            AttackInput = _enemyApproachObserver.IsClose;
            MoveInput = _enemyApproachObserver.IsFar;
        }

        private void OnEnable() =>
            Enable();

        private void OnDisable() =>
            Disable();

        public void Enable() =>
            _enemyApproachObserver.Enable();

        public void Disable() =>
            _enemyApproachObserver.Disable();
    }
}