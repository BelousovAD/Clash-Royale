using System;
using UnityEngine;

namespace Currency
{
    public abstract class Currency
    {
        protected const int Min = 0;

        private int _value;

        public Currency(CurrencyType type, int defaultValue = Min, int max = int.MaxValue)
        {
            Type = type;
            Max = max;
            Value = defaultValue;
        }

        public event Action Changed;

        public CurrencyType Type { get; }

        public int Max { get; }

        public int Value
        {
            get
            {
                return _value;
            }

            private set
            {
                if (value != _value)
                {
                    _value = Mathf.Clamp(value, Min, Max);
                    Changed?.Invoke();
                }
            }
        }
        
        public void Earn(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Can not be negative");
            }

            Value += amount;
        }

        public bool TrySpend(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Can not be negative");
            }

            if (Value < amount)
            {
                return false;
            }

            Value -= amount;
            return true;
        }
    }
}