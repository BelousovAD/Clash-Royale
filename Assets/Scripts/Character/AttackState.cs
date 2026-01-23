using Inputs;

namespace Character
{
    public class AttackState : AbstractCharacterAnimatorState
    {
        private const float BusyTime = 1f;

        private readonly Unit _character;
        private readonly IInputReader _inputReader;
        private float _busynessCountdown;

        public AttackState(Unit character, IInputReader inputReader)
            : base(character.Animator)
        {
            _character = character;
            _inputReader = inputReader;
        }

        public override void Enter()
        {
            IsBusy = true;
            _inputReader.LockMove();

            CharacterAnimator.Play(CharacterAnimator.AnimationKey.Attack);

            _busynessCountdown = BusyTime;
            base.Enter();
        }

        public override void Exit()
        {
            _inputReader.UnlockMove();
            base.Exit();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_busynessCountdown > 0f)
            {
                _busynessCountdown -= deltaTime;

                if (_busynessCountdown <= 0f)
                {
                    IsBusy = false;
                }
            }
        }
    }
}