using System;
using FSM;
using UnityEngine;

namespace Unit
{
    internal class Unit : MonoBehaviour
    {
        private StateMachine _stateMachine;

        public event Action Initialized;

        public Health Health { get; private set; }

        public IStateSwitcher StateSwitcher => _stateMachine;

        private void Update() =>
            _stateMachine?.Update(Time.deltaTime);

        private void LateUpdate() =>
            _stateMachine?.LateUpdate(Time.deltaTime);

        private void FixedUpdate() =>
            _stateMachine?.FixedUpdate(Time.fixedTime);

        public void Initialize(int health) =>
            Health = new Health(health);

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            Initialized?.Invoke();
        }
    }
}