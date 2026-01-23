using FSM;

namespace Character
{
    public class DyingState : AbstractState
    {
        private const float BusyTime = 1f;

        private readonly CharacterAnimator _character;

        public DyingState(CharacterAnimator character)
        {
            _character = character;
        }

        public override void Enter()
        {
            IsBusy = true;

            _character.Play(CharacterAnimator.AnimationKey.Die);
            
            base.Enter();
        }
    }
}