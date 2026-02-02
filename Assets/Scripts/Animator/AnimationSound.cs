using Audio;
using Character;
using UnityEngine;

namespace Animator
{
    internal class AnimationSound : StateMachineBehaviour
    {
        [SerializeField][Range(0f, 1f)] private float _soundMark;
        [SerializeField] private AudioClipKey _key;
        [SerializeField] private bool _isRangeChar;

        private ProjectileAnimation _projectileAnimation;
        private CharacterSound _characterSound;
        private float _delay;
        private float _timeToEnd;
        private float _time;
        private bool _isAudioPlayed;

        public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _characterSound ??= animator.GetComponent<CharacterSound>();
            _projectileAnimation ??= animator.GetComponent<ProjectileAnimation>();
            
            _time = 0;
            _isAudioPlayed = false;
            _delay = stateInfo.length * _soundMark;
            _timeToEnd = stateInfo.length - _delay;
        }

        public override void OnStateUpdate(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _time += Time.deltaTime;

            if (_isAudioPlayed == true)
            {
                if (_time >= _timeToEnd)
                {
                    _time = 0;
                    _isAudioPlayed = false;
                }
            }
            else
            {
                if (_time >= _delay && _characterSound.Source.isPlaying == false)
                {
                    if (_isRangeChar)
                    {
                        _projectileAnimation.SendProjectile();
                    }
                    
                    _characterSound.PlayTrack(_key);
                    _time = 0;
                    _isAudioPlayed = true;
                }
            }
        }

        public override void OnStateExit(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            _characterSound.Source.Stop();
    }
}