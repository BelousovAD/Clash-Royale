using System;
using Castle;
using UnityEngine;

namespace Inputs
{
    internal class AttackInput : MonoBehaviour
    {
        private Transform _target;

        public event Action<Transform> TargetFound;

        private void OnTriggerEnter(Collider other)
        {
            if (_target != null)
            {
                if (other.gameObject.TryGetComponent(out Tower tower))
                {
                    _target = tower;
                    TargetFound?.Invoke(_target);
                }
            }
        }

    }
}