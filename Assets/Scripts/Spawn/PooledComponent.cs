namespace Spawn
{
    using System;
    using UnityEngine;

    public class PooledComponent : MonoBehaviour
    {
        public event Action<PooledComponent> ReleaseRequested;

        public void Release() =>
            ReleaseRequested?.Invoke(this);
    }
}