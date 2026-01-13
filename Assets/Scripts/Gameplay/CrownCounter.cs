using System;
using UnityEngine;

namespace Gameplay
{
    internal class CrownCounter
    {
        private const int MinCount = 0;
        private const int MaxCount = 3;
        
        private int _count = MaxCount;

        public CrownCounter(CrownType type) =>
            Type = type;

        public event Action CountChanged;

        public int Count
        {
            get
            {
                return _count;
            }

            private set
            {
                if (value != _count)
                {
                    _count = Mathf.Clamp(value, MinCount, MaxCount);
                    CountChanged?.Invoke();
                }
            }
        }
        
        public CrownType Type { get; }

        public void CountDown() =>
            Count--;
    }
}