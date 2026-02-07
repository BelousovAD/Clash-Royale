using System;
using Bootstrap;

namespace Tutorial
{
    internal class Tutorial
    {
        private const int Min = 0;
        
        private readonly int _maxStage;
        private readonly string _saveKey;
        private SavvyServicesProvider _services;

        public Tutorial(string saveKey, int maxStage)
        {
            if (maxStage <= Min)
            {
                throw new ArgumentOutOfRangeException(nameof(maxStage), maxStage, null);
            }
            
            _saveKey = saveKey;
            _maxStage = maxStage;
        }

        public bool IsCompleted => _services.Preferences.LoadBool(_saveKey);

        public int LastStage { get; private set; }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Complete(int stage)
        {
            LastStage = stage;

            if (LastStage == _maxStage)
            {
                _services.Preferences.SaveBool(_saveKey, true);
            }
        }
    }
}