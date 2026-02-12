using System;
using Bootstrap;

namespace Currency
{
    public class SaveableCurrency : Currency, IDisposable
    {
        private SavvyServicesProvider _services;

        public SaveableCurrency(CurrencyType type, int defaultValue = Min, int max = int.MaxValue)
            : base(type, defaultValue, max) =>
            Changed += Save;

        public void Dispose() =>
            Changed -= Save;

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Load() =>
            Earn(_services.Preferences.LoadInt(Type.ToString(), Value));

        private void Save() =>
            _services.Preferences.SaveInt(Type.ToString(), Value);
    }
}