using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Animator))]
    internal class UnitAnimator : MonoBehaviour
    {
        private static readonly int AttackFactor = Animator.StringToHash(nameof(AttackFactor));

        private Animator _animator;
        private Dictionary<AnimationKey, int> _parameters;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _parameters = new Dictionary<AnimationKey, int>
            {
                [AnimationKey.Idle] = Animator.StringToHash(nameof(AnimationKey.Idle)),
                [AnimationKey.Attack] = Animator.StringToHash(nameof(AnimationKey.Attack)),
                [AnimationKey.Die] = Animator.StringToHash(nameof(AnimationKey.Die)),
                [AnimationKey.Run] = Animator.StringToHash(nameof(AnimationKey.Run)),
            };
        }

        private void OnDestroy() =>
            _animator = null;

        public void Play(AnimationKey animationKey)
        {
            if (_animator is null)
            {
                return;
            }

            foreach (int parameter in _parameters.Values)
            {
                _animator.SetBool(parameter, false);
            }

            if (_parameters.TryGetValue(animationKey, out int parameterId))
            {
                _animator.SetBool(parameterId, true);
            }
        }

        public void SetAttackFactor(float speed) =>
            _animator.SetFloat(AttackFactor, speed);
    }
}