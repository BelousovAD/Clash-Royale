using System.Collections;
using Spawn;
using UnityEngine;

namespace UnitSpawn
{
    internal class UnitReleaser : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private PooledComponent _pooledComponent;
        [SerializeField][Min(0f)] private float _deathAnimationDuration;
        
        private WaitForSeconds _wait;

        private void Awake() =>
            _wait = new WaitForSeconds(_deathAnimationDuration);

        private void OnEnable()
        {
            _unit.Initialized += Subscribe;
            Subscribe();
        }

        private void OnDisable() =>
            Unsubscribe();
        
        private void Subscribe()
        {
            if (_unit.Health is not null)
            {
                _unit.Initialized -= Subscribe;
                _unit.Health.Changed += Die;
                Die();
            }
        }

        private void Unsubscribe()
        {
            _unit.Initialized -= Subscribe;

            if (_unit.Health is not null)
            {
                _unit.Health.Changed -= Die;
            }
        }
        
        private void Die()
        {
            if (_unit.Health.IsDead)
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