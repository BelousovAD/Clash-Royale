using System;
using System.Collections;
using FSM;
using Spawn;
using UnityEngine;

namespace Unit
{
    internal class Unit : PooledComponent
    {
        private StateMachine _stateMachine;
        private WaitForSeconds _wait;

        public event Action Initialized;

        public Health Health { get; protected set; }

        public IStateSwitcher StateSwitcher => _stateMachine;

        private void OnEnable() =>
            Health.Changed += Die;

        private void OnDisable() =>
            Health.Changed -= Die;

        private void Update() =>
            _stateMachine?.Update(Time.deltaTime);

        private void LateUpdate() =>
            _stateMachine?.LateUpdate(Time.deltaTime);

        private void FixedUpdate() =>
            _stateMachine?.FixedUpdate(Time.fixedTime);

        public void Initialize(StateMachine stateMachine, float releaseDelay, int health)
        {
            _stateMachine = stateMachine;
            _wait = new WaitForSeconds(releaseDelay);
            Health = new Health(health);
            Initialized?.Invoke();
        }

        private void Die()
        {
            if (Health.Value <= 0)
            {
                StartCoroutine(DieAfterDelay());
            }
        }

        private IEnumerator DieAfterDelay()
        {
            yield return _wait;
            
            Release();
        }
    }
}