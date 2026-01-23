using FSM;

namespace Character
{
    public class AbstractCharacterAnimatorState : AbstractState
    {
        protected readonly CharacterAnimator CharacterAnimator;

        public AbstractCharacterAnimatorState(CharacterAnimator characterAnimator) =>
            CharacterAnimator = characterAnimator;
    }
}