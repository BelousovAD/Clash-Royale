using System.Collections.Generic;
using Common;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [RequireComponent(typeof(Button))]
    internal class SkipGameButton : AbstractButton
    {
        private const CrownType EnemyCrown = CrownType.Enemy;
        private const CrownType PlayerCrown = CrownType.Enemy;
        
        [SerializeField] private bool _win;

        private Gameplay _gameplay;
        private CrownCounter _enemyCrownCounter;
        private CrownCounter _playerCrownCounter;

        [Inject]
        private void Initialize(Gameplay gameplay, IEnumerable<CrownCounter> crownCounters)
        {
            _gameplay = gameplay;

            foreach (CrownCounter crownCounter in crownCounters)
            {
                if (crownCounter.Type == EnemyCrown)
                {
                    _enemyCrownCounter = crownCounter;
                }

                if (crownCounter.Type == PlayerCrown)
                {
                    _playerCrownCounter = crownCounter;
                }
            }
        }

        protected override void HandleClick()
        {
            if (_win)
            {
                _playerCrownCounter.CountUp();
            }
            else
            {
                _enemyCrownCounter.CountUp();
            }
            
            _gameplay.FinishGame();
        }
    }
}