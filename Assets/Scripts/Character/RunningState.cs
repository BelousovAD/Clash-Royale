using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    public class RunningState : AbstractCharacterAnimatorState
    {
        public RunningState(CharacterAnimator characterAnimator)
            : base(characterAnimator)
        { }

        public override void Enter()
        {
            CharacterAnimator.Play(CharacterAnimator.AnimationKey.Run);
            base.Enter();
        }
    }
}