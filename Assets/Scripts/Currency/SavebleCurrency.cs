using Bootstrap;

namespace Currency
{
    public class SavebleCurrency : Currency
    {
        private SavvyServicesProvider _services;

        public SavebleCurrency(CurrencyType type) : base(type) =>
            Changed += Save;

        public void Dispose() =>
            Changed -= Save;
        
        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        public void Load() =>
            Earn(_services.Preferences.LoadInt(nameof(Type), Min));

        private void Save() =>
            _services.Preferences.SaveInt(nameof(Type), Value);
    }
}