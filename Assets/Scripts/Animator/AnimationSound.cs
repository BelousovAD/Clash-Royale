using Audio;
using Character;
using UnityEngine;

namespace Animator
{
    internal class AnimationSound : StateMachineBehaviour
    {
        [SerializeField] private AudioClipKey _key;
        [SerializeField] private bool _needDelay;

        private float _delay;
        private CharacterSound _characterSound;
        private float _time;
        private bool _isAudioStart;

        public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _characterSound = animator.GetComponent<CharacterSound>();
            _time = 0;
            _isAudioStart = false;

            if (_needDelay)
            {
                _delay = stateInfo.length / 2;
            }
        }

        public override void OnStateUpdate(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isAudioStart == false)
            {
                _time += Time.deltaTime;

                if (_time > _delay)
                {
                    _characterSound.PlayTrack(_key);
                    _isAudioStart = true;
                }
            }
        }
    }
}