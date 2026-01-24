using System;
using UnityEngine;

namespace Unit
{
    internal class Health : ChangeableValue.ChangeableValue<float>
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

        private float MaxValue { get; }

        public new float Value
        {
            get => base.Value;
            private set => base.Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool IsDead => Value <= MinValue;

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Can not be negative");
            }

            Value -= damage;
        }

        public void TakeHealing(float healing)
        {
            if (healing < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(healing), "Can not be negative");
            }

            Value += healing;
        }
    }
}