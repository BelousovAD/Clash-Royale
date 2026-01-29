using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Animator))]
    internal class UnitAnimator : MonoBehaviour
    {
        private static readonly int AttackSpeed = Animator.StringToHash(nameof(AttackSpeed));

        [SerializeField] private List<AnimationKey> _keys;

        private Animator _animator;
        private readonly Dictionary<AnimationKey, int> _parameters = new ();

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            foreach (AnimationKey key in _keys)
            {
                _parameters.Add(key, Animator.StringToHash(key.ToString()));
            }
        }

        private void OnDestroy() =>
            _animator = null;

        public void Play(AnimationKey animationKey)
        {
            if (_animator is null)
            {
                return;
            }

            if (_parameters.TryGetValue(animationKey, out int parameterId) == false)
            {
                Debug.Log($"{animationKey} is not declared in field {nameof(_keys)} and can not be played");
                return;
            }
            
            foreach (int parameter in _parameters.Values)
            {
                _animator.SetBool(parameter, false);
            }
                
            _animator.SetBool(parameterId, true);
        }

        public void SetAttackSpeed(float speed) =>
            _animator.SetFloat(AttackSpeed, speed);
    }
}