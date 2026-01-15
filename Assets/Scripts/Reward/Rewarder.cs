using System;
using UnityEngine;

namespace Reward
{
    internal abstract class Rewarder : IDisposable
    {
        private readonly RewardData _data;
        private Gameplay.Judge _judge;

        public Rewarder(RewardData data)
        {
            _data = data;
            Icon = _data.Icon;
        }

        public event Action Rewarded;

        public Sprite Icon { get; private set; }

        public bool IsVictory => _judge.IsVictory != null && (bool)_judge.IsVictory;

        public int LoseAmount => _data.LoseAmount;

        public RewardType Type => _data.Type;
        
        public int WinAmount => _data.WinAmount;

        public void Initialize(Gameplay.Judge judge)
        {
            _judge = judge;
            _judge.VictoryStatusChanged += StartRewarding;
        }

        public void Dispose() =>
            _judge.VictoryStatusChanged -= StartRewarding;

        public void UpdateIcon(Sprite sprite) =>
            Icon = sprite;

        private void StartRewarding()
        {
            _judge.VictoryStatusChanged -= StartRewarding;
            
            if (IsVictory)
            {
                ApplyReward();
            }
            else
            {
                ApplyPenalty();
            }
            
            Rewarded?.Invoke();
        }

        protected abstract void ApplyPenalty();

        protected abstract void ApplyReward();
    }
}