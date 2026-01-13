using System;
using UnityEngine;

namespace Gameplay
{
    internal class CrownCounter
    {
        public const int Max = 3;
        private const int Min = 0;
        
        private int _count;

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
                    _count = Mathf.Clamp(value, Min, Max);
                    CountChanged?.Invoke();
                }
            }
        }
        
        public CrownType Type { get; }

        public void CountUp() =>
            Count++;
    }
}