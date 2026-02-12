using System;
using System.Collections.Generic;
using System.Linq;
using ChangeableValue;
using Timer;
using Window;

namespace Gameplay
{
    public class Judge : IDisposable
    {
        private const int CountToClose = 0;
        private const CrownType EnemyCrown = CrownType.Enemy;
        private const CrownType PlayerCrown = CrownType.Player;
        
        private readonly string _endgameWindowId;
        private readonly IsMainTowerAlive _isEnemyMainTowerAlive;
        private readonly IsMainTowerAlive _isPlayerMainTowerAlive;
        private readonly List<Unit.Unit> _enemyTowers;
        private readonly List<Unit.Unit> _playerTowers;
        private CoroutineTimer _timer;
        private IWindowService _windowService;
        private CrownCounter _enemyCrownCounter;
        private CrownCounter _playerCrownCounter;
        private bool? _isVictory;

        public Judge(
            string endgameWindowId,
            Unit.Unit enemyMainTower,
            Unit.Unit playerMainTower,
            IEnumerable<Unit.Unit> enemyTowers,
            IEnumerable<Unit.Unit> playerTowers)
        {
            _endgameWindowId = endgameWindowId;
            _isEnemyMainTowerAlive = new IsMainTowerAlive(enemyMainTower);
            _isPlayerMainTowerAlive = new IsMainTowerAlive(playerMainTower);
            _enemyTowers = new List<Unit.Unit>(enemyTowers);
            _playerTowers = new List<Unit.Unit>(playerTowers);
        }

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

            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
            _isEnemyMainTowerAlive.Dispose();
            _isPlayerMainTowerAlive.Dispose();
        }

        private void FinishGame()
        {
            if (_isEnemyMainTowerAlive.Value == false)
            {
                IsVictory = true;
            }
            else if (_isPlayerMainTowerAlive.Value == false)
            {
                IsVictory = false;
            }
            else if (_playerCrownCounter.Count != _enemyCrownCounter.Count)
            {
                IsVictory = _playerCrownCounter.Count > _enemyCrownCounter.Count;
            }
            else
            {
                IsVictory = _playerTowers.Sum(tower => tower.Health.Value) >
                            _enemyTowers.Sum(tower => tower.Health.Value);
            }
            
            _windowService.Open(_endgameWindowId, CountToClose);
            Unsubscribe();
        }

        private void Subscribe()
        {
            _timer.TimeIsUp += FinishGame;
            _isEnemyMainTowerAlive.Changed += FinishGame;
            _isPlayerMainTowerAlive.Changed += FinishGame;
        }

        private void Unsubscribe()
        {
            _timer.TimeIsUp -= FinishGame;
            _isEnemyMainTowerAlive.Changed -= FinishGame;
            _isPlayerMainTowerAlive.Changed -= FinishGame;
        }
    }
}