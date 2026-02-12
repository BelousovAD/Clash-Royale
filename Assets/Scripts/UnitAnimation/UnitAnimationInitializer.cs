using Unit;
using UnityEngine;

namespace UnitAnimation
{
    public class UnitAnimationInitializer : MonoBehaviour
    {
        [SerializeField] private UnitAnimationCaller _animationCaller;
        [SerializeField] private UnitPrefabView _unitPrefabView;

        private UnitAnimator _unitAnimator;
        private float _attackSpeed;
        
        private void OnEnable()
        {
            _unitPrefabView.InstanceChanged += Initialize;
            Initialize();
        }

        private void OnDisable() =>
            _unitPrefabView.InstanceChanged -= Initialize;

        public void UpdateAttackSpeed(float attackSpeed)
        {
            _attackSpeed = attackSpeed;
            _unitAnimator?.SetAttackSpeed(_attackSpeed);
        }

        private void Initialize()
        {
            if (_unitPrefabView.Instance is not null)
            {
                _unitAnimator = _unitPrefabView.Instance.GetComponent<UnitAnimator>();
                _unitAnimator.SetAttackSpeed(_attackSpeed);
                _animationCaller.Initialize(_unitAnimator);
            }
        }
    }
}