using System;
using Bootstrap;

namespace Currency
{
    public class SavebleCurrency : Currency, IDisposable
    {
        private SavvyServicesProvider _services;

        public SavebleCurrency(CurrencyType type, int defaultValue = Min, int max = int.MaxValue)
            : base(type, defaultValue, max) =>
            Changed += Save;

        public void Dispose() =>
            Changed -= Save;

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Load() =>
            Earn(_services.Preferences.LoadInt(nameof(Type), Value));

        private void Save() =>
            _services.Preferences.SaveInt(nameof(Type), Value);
    }
}