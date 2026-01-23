using System;
using Common;
using UnityEngine;

namespace Character
{
    public class Health : ChangeableValue<float>
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

        public override float Value
        {
            get => base.Value;
            protected set => base.Value = Mathf.Clamp(value, MinValue, MaxValue);
        }

        public bool IsDead()
        {
            return Value <= 0;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage can't be negative");
            }

            Value -= damage;
        }

        public void TakeHealing(float healing)
        {
            if (healing < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(healing), "Healing can't be negative");
            }

            Value += healing;
        }
    }
}