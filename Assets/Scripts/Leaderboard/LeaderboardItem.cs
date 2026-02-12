using System;
using UnityEngine;

namespace Leaderboard
{
    internal class LeaderboardItem : MonoBehaviour
    {
        private const int Min = 0;
        private const int MinPosition = 1;

        public event Action Initialized;
        public event Action IconChanged;

        public int Position { get; private set; } = MinPosition;

        public string IconUrl { get; private set; }

        public string Name { get; private set; }

        public int Score { get; private set; }

        public bool IsInTop { get; private set; }

        public bool IsCurrentPlayer { get; private set; }

        public Sprite Icon { get; private set; }

        public void Initialize(
            int position,
            string iconUrl,
            string nickname,
            int score,
            bool isInTop,
            bool isCurrentPlayer)
        {
            if (position < MinPosition)
            {
                throw new ArgumentOutOfRangeException(nameof(position), $"Must be greater than {MinPosition}");
            }

            if (score < Min)
            {
                throw new ArgumentOutOfRangeException(nameof(score), $"Must be positive");
            }

            Position = position;
            IconUrl = iconUrl;
            Name = nickname;
            Score = score;
            IsInTop = isInTop;
            IsCurrentPlayer = isCurrentPlayer;
            Initialized?.Invoke();
        }

        public void SetIcon(Sprite sprite)
        {
            Icon = sprite;
            IconChanged?.Invoke();
        }
    }
}