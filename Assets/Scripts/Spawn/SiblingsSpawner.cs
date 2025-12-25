namespace Spawn
{
    using System;
    using UnityEngine;
    using UnityEngine.Pool;

    public class SiblingsSpawner : MonoBehaviour
    {
        [SerializeField] private PooledComponent _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField, Min(1)] private int _poolSize = 20;

        private IObjectPool<PooledComponent> _pool;
        
        public event Action<PooledComponent> ComponentReleased;

        private void Awake()
        {
            _pool = new ObjectPool<PooledComponent>(
                createFunc: CreatePooledComponent,
                actionOnRelease: ReleasePooledComponent,
                actionOnDestroy: DestroyPooledComponent,
                defaultCapacity: _poolSize);
            _parent ??= transform;
        }

        public void Initialize(PooledComponent prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent ?? transform;
        }

        public PooledComponent Spawn()
        {
            PooledComponent pooledComponent = _pool.Get();
            pooledComponent.ReleaseRequested += _pool.Release;
            pooledComponent.transform.SetAsLastSibling();
            pooledComponent.gameObject.SetActive(true);

            return pooledComponent;
        }

        private void ReleasePooledComponent(PooledComponent pooledComponent)
        {
            pooledComponent.gameObject.SetActive(false);
            pooledComponent.ReleaseRequested -= _pool.Release;
            ComponentReleased?.Invoke(pooledComponent);
        }

        private PooledComponent CreatePooledComponent()
        {
            PooledComponent pooledComponent = Instantiate(_prefab, _parent);

            return pooledComponent;
        }

        private static void DestroyPooledComponent(PooledComponent pooledComponent) =>
            Destroy(pooledComponent.gameObject);
    }
}
