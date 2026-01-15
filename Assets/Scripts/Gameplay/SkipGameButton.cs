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
        private const CrownType PlayerCrown = CrownType.Player;
        
        [SerializeField] private bool _win;

        private Judge _judge;
        private CrownCounter _enemyCrownCounter;
        private CrownCounter _playerCrownCounter;

        [Inject]
        private void Initialize(Judge judge, IEnumerable<CrownCounter> crownCounters)
        {
            _judge = judge;

            foreach (CrownCounter counter in crownCounters)
            {
                if (counter.Type == EnemyCrown)
                {
                    _enemyCrownCounter = counter;
                }

                if (counter.Type == PlayerCrown)
                {
                    _playerCrownCounter = counter;
                }
            }
        }

        protected override void HandleClick()
        {
            if (_win)
            {
                for (int i = 0; i < CrownCounter.Max; i++)
                {
                    _playerCrownCounter.CountUp();
                }
            }
            else
            {
                for (int i = 0; i < CrownCounter.Max; i++)
                {
                    _enemyCrownCounter.CountUp();
                }
            }
            
            _judge.FinishGame();
        }
    }
}