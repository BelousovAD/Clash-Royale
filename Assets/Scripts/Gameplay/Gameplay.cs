using System;
using System.Collections.Generic;
using System.Linq;
using Timer;
using Window;

namespace Gameplay
{
    internal class Gameplay : IDisposable
    {
        private readonly string _endgameWindowId;
        private CoroutineTimer _timer;
        private IWindowService _windowService;
        private List<CrownCounter> _crownCounters;

        public Gameplay(string endgameWindowId) =>
            _endgameWindowId = endgameWindowId;

        public void Initialize(
            IEnumerable<CrownCounter> crownCounters,
            CoroutineTimer timer,
            IWindowService windowService)
        {
            _timer = timer;
            _windowService = windowService;
            _crownCounters = new List<CrownCounter>(crownCounters);

            _crownCounters.ForEach(counter => counter.CountChanged += FinishGame);
            _timer.TimeIsUp += FinishGame;
        }

        public void Dispose()
        {
            _crownCounters.ForEach(counter => counter.CountChanged -= FinishGame);
            _timer.TimeIsUp -= FinishGame;
        }

        public void FinishGame()
        {
            if (IsAnyCounterFull() || _timer.IsTimeUp)
            {
                _windowService.Open(_endgameWindowId, false);
                Dispose();
            }
        }

        private bool IsAnyCounterFull() =>
            _crownCounters.Any(counter => counter.Count == CrownCounter.Max);
    }
}