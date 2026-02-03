using System.Collections;
using FSM;
using Spawn;
using UnityEngine;

namespace UnitSpawn
{
    internal class UnitReleaser : MonoBehaviour
    {
        private const StateType DieState = StateType.Die;
        
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private PooledComponent _pooledComponent;
        [SerializeField][Min(0f)] private float _deathAnimationDuration;
        
        private WaitForSeconds _wait;
        private IStateSwitcher _stateSwitcher;

        private void Awake() =>
            _wait = new WaitForSeconds(_deathAnimationDuration);

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _unit.Initialized -= UpdateSubscriptions;

            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }
        }

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += Die;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= Die;

        private void UpdateSubscriptions()
        {
            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }

            _stateSwitcher = _unit.StateSwitcher;

            if (_stateSwitcher is not null)
            {
                Subscribe();
            }
            
            Die();
        }
        
        private void Die()
        {
            if (_stateSwitcher?.CurrentState.Type == DieState)
            {
                StartCoroutine(DieAfterDelay());
            }
        }

        private IEnumerator DieAfterDelay()
        {
            yield return _wait;
            
            _pooledComponent.Release();
        }
    }
}