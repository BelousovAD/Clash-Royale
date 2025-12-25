namespace Currency
{
    using System;
    using Bootstrap;

    public class Money
    {
        private const int Min = 0;
        private const string SaveKey = nameof(Money);

        private int _value;
        private SavvyServicesProvider _services;

        public event Action Changed;

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
            Value = _services.Preferences.LoadInt(SaveKey, Min);

        private void Save() =>
            _services.Preferences.SaveInt(SaveKey, Value);
    }
}