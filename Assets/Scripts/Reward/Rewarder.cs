using System;
using UnityEngine;

namespace Reward
{
    internal abstract class Rewarder : IDisposable
    {
        private readonly RewardData _data;
        private Gameplay.Gameplay _gameplay;

        public Rewarder(RewardData data)
        {
            _data = data;
            Icon = _data.Icon;
        }

        public event Action Rewarded;

        public Sprite Icon { get; private set; }

        public bool IsVictory => _gameplay.IsVictory != null && (bool)_gameplay.IsVictory;

        public int LoseAmount => _data.LoseAmount;

        public RewardType Type => _data.Type;
        
        public int WinAmount => _data.WinAmount;

        public void Initialize(Gameplay.Gameplay gameplay)
        {
            _gameplay = gameplay;
            _gameplay.VictoryStatusChanged += StartRewarding;
        }

        public void Dispose() =>
            _gameplay.VictoryStatusChanged -= StartRewarding;

        public void UpdateIcon(Sprite sprite) =>
            Icon = sprite;

        private void StartRewarding()
        {
            _gameplay.VictoryStatusChanged -= StartRewarding;
            
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