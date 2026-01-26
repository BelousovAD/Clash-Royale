using Audio;
using Character;
using UnityEngine;

namespace Animator
{
    public class AnimationSound : StateMachineBehaviour
    {
        [SerializeField] private AudioClipKey _key;
        [SerializeField] private bool _needDelay;

        private float _delay;
        private CharacterSound _characterSound;
        private float _time;
        private bool _isAudioStart;

        override public void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _characterSound = animator.GetComponent<CharacterSound>();
            _time = 0;
            _isAudioStart = false;

            if (_needDelay)
            {
                _delay = stateInfo.length / 2;
            }
        }

        override public void OnStateUpdate(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}