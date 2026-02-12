using System;
using UnityEngine;

namespace Unit
{
    public class Health : ChangeableValue.ChangeableValue<float>
    {
        private const float MinValue = 0;

        public Health(float maxValue)
        {
            if (maxValue <= MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue), "Must be positive");
            }

            MaxValue = maxValue;
            Value = MaxValue;
        }

        public float MaxValue { get; }

        public new float Value
        {
            get => base.Value;
            private set => base.Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool IsDead => Value <= MinValue;

        public void Take(float amount)
        {
            if (IsDead == false)
            {
                Value += amount;
            }
        }
    }
}