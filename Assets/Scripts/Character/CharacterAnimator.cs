using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Dictionary<AnimationKey, int> _parameters;

        public enum AnimationKey
        {
            Idle = 0,
            Attack = 1,
            Die = 2,
            Run = 3,
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _parameters = new Dictionary<AnimationKey, int>
            {
                [AnimationKey.Idle] = Animator.StringToHash(nameof(AnimationKey.Idle)),
                [AnimationKey.Run] = Animator.StringToHash(nameof(AnimationKey.Run)),
                [AnimationKey.Attack] = Animator.StringToHash(nameof(AnimationKey.Attack)),
                [AnimationKey.Die] = Animator.StringToHash(nameof(AnimationKey.Die)),
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
    }

}