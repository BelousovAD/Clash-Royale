using System.Collections;
using Spawn;
using UnityEngine;

namespace Unit
{
    internal class UnitReleaser : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private PooledComponent _pooledComponent;
        
        private WaitForSeconds _wait;

        public void Initialize(float releaseDelay) =>
            _wait = new WaitForSeconds(releaseDelay);

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