using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace EnemyObserve
{
    internal class IsTransformClose : ChangeableValue<bool?>, IEnable, IDisable, IUpdatable
    {
        private Transform _transformFrom;
        private Transform _transformTo;
        private float _sqrCloseDistance;
        private bool _isInitialized;
        private bool _isActive;

        public void Initialize(Transform from, float closeDistance)
        {
            _transformFrom = from;
            _sqrCloseDistance = closeDistance * closeDistance;
            _isInitialized = true;
        }

        public void SetDestination(Transform to) =>
            _transformTo = to;

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
            
            if (_transformTo is null)
            {
                Value = null;
            }
            else
            {
                Value = Vector3.SqrMagnitude(_transformTo.position - _transformFrom.position) <= _sqrCloseDistance;
            }
        }
    }
}