using Character;
using UnityEngine;

namespace UnitAnimation
{
    internal class TowerAnimationInitializer : MonoBehaviour
    {
        [SerializeField] private UnitAnimationCaller _animationCaller;
        [SerializeField] private CharacterData _data;
        [SerializeField] private UnitAnimator _unitAnimator;
        
        private void Start() =>
            Initialize();

        private void Initialize()
        {
            _unitAnimator.SetAttackSpeed(_data.AttackSpeed);
            _animationCaller.Initialize(_unitAnimator);
        }
    }
}