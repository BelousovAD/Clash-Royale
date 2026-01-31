using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace Unit
{
    public class IsTransformClose : ChangeableValue<bool?>, IEnable, IDisable, IUpdatable
    {
        private Transform _transformFrom;
        private Transform _transformTarget;
        private float _targetRadius;
        private float _closeDistance;
        private bool _isActive;

        public void Initialize(Transform from) =>
            _transformFrom = from;

        public void Initialize(float closeDistance) =>
            _closeDistance = closeDistance;

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
            if (_isActive == false)
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