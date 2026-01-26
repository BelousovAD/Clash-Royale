using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace EnemyObserve
{
    internal class IsTransformClose : ChangeableValue<bool?>, IEnable, IDisable, IUpdatable
    {
        private Transform _transformFrom;
        private Transform _transformTarget;
        private float _targetRadius;
        private float _closeDistance;
        private bool _isInitialized;
        private bool _isActive;

        public void Initialize(Transform from, float closeDistance)
        {
            _transformFrom = from;
            _closeDistance = closeDistance;
            _isInitialized = true;
        }

        public void SetTarget(Transform target, float targetRadius)
        {
            _transformTarget = target;
            _targetRadius = targetRadius;
        }

        public void Enable()
        {
            Value = null;
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
            Value = null;
        }

        public void Update(float deltaTime)
        {
            if (!_isInitialized || !_isActive)
            {
                return;
            }
            
            if (_transformTarget is null)
            {
                Value = null;
            }
            else
            {
                Value = Vector3.Magnitude(_transformTarget.position - _transformFrom.position)
                        - _targetRadius <= _closeDistance;
            }
        }
    }
}