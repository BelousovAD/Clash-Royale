using System;
using Bootstrap;

namespace Currency
{
    public class Currency
    {
        private const int Min = 0;

        private int _value;
        private SavvyServicesProvider _services;

        public Currency(CurrencyType type) =>
            Type = type;

        public event Action Changed;

        public CurrencyType Type { get; }
        
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value != _value)
                {
                    _value = value < Min ? Min : value;
                    Save();
                    Changed?.Invoke();
                }
            }
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

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

        public void Load() =>
            Value = _services.Preferences.LoadInt(nameof(Type), Min);

        private void Save() =>
            _services.Preferences.SaveInt(nameof(Type), Value);
    }
}