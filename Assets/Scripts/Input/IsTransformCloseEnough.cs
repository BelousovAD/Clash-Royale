using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace Input
{
    internal class IsTransformCloseEnough : ChangeableValue<bool>, IEnable, IDisable, IUpdatable
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
            Value = false;
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
            Value = false;
        }

        public void Update(float deltaTime)
        {
            if (_isInitialized && _isActive && _transformTo is not null)
            {
                Value = Vector3.SqrMagnitude(_transformTo.position - _transformFrom.position) <= _sqrCloseDistance;
            }
        }
    }
}