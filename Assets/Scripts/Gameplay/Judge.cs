using System;
using System.Collections.Generic;
using Timer;
using Window;

namespace Gameplay
{
    public class Judge : IDisposable
    {
        private const CrownType EnemyCrown = CrownType.Enemy;
        private const CrownType PlayerCrown = CrownType.Player;
        
        private readonly string _endgameWindowId;
        private CoroutineTimer _timer;
        private IWindowService _windowService;
        private CrownCounter _enemyCrownCounter;
        private CrownCounter _playerCrownCounter;
        private bool? _isVictory;

        public Judge(string endgameWindowId) =>
            _endgameWindowId = endgameWindowId;

        public event Action VictoryStatusChanged;

        public bool? IsVictory
        {
            get
            {
                return _isVictory;
            }

            private set
            {
                if (value != _isVictory)
                {
                    _isVictory = value;
                    VictoryStatusChanged?.Invoke();
                }
            }
        }

        public void Initialize(
            IEnumerable<CrownCounter> crownCounters,
            CoroutineTimer timer,
            IWindowService windowService)
        {
            _timer = timer;
            _windowService = windowService;

            foreach (CrownCounter counter in crownCounters)
            {
                switch (counter.Type)
                {
                    case EnemyCrown:
                        _enemyCrownCounter = counter;
                        break;
                    case PlayerCrown:
                        _playerCrownCounter = counter;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _enemyCrownCounter.CountChanged += FinishGame;
            _playerCrownCounter.CountChanged += FinishGame;
            _timer.TimeIsUp += FinishGame;
        }

        public void Dispose()
        {
            _enemyCrownCounter.CountChanged -= FinishGame;
            _playerCrownCounter.CountChanged -= FinishGame;
            _timer.TimeIsUp -= FinishGame;
        }

        public void FinishGame()
        {
            if (IsAnyCounterFull() || _timer.IsTimeUp)
            {
                IsVictory = _playerCrownCounter.Count > _enemyCrownCounter.Count;
                _windowService.Open(_endgameWindowId, false);
                Dispose();
            }
        }

        private bool IsAnyCounterFull() =>
            _enemyCrownCounter.Count == CrownCounter.Max || _playerCrownCounter.Count == CrownCounter.Max;
    }
}