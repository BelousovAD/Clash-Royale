using System;
using UnityEngine;

namespace Spawn
{
    public class PooledComponent : MonoBehaviour
    {
        public event Action<PooledComponent> ReleaseRequested;

        public void Release() =>
            ReleaseRequested?.Invoke(this);
    }
}