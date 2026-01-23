using System;
using Common;
using UnityEngine;

namespace Inputs
{
    public class InputReader : MonoBehaviour, IInputReader
    {
        [SerializeField] private TowerFounder _towerFounder;

        public ChangeableValue<bool> AttackInput { get; private set; }
        public ChangeableValue<Vector2> LookInput => throw new NotImplementedException();
        public ChangeableValue<Vector2> MoveInput { get; private set; }

        private void Awake()
        {
            AttackInput = _towerFounder.IsCloseEnough;
            MoveInput = _towerFounder.FindTarget();
        }

        private void OnEnable() =>
            Enable();

        private void OnDisable() =>
            Disable();

        public void LockMove() =>
            _towerFounder.ForgetDirection();

        public void UnlockMove() =>
            _towerFounder.RemindDirection();

        public void Enable()
        {
            _towerFounder.FindTarget();
        }

        public void Disable() =>
            _towerFounder.Disable();
    }
}