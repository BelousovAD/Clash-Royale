using System;
using Bootstrap;
using MirraGames.SDK;
using MirraGames.SDK.Common;

namespace Nickname
{
    internal class Player : Opponent, IDisposable
    {
        private readonly IPlayer _data;
        private readonly string _defaultNameLocalizationKey;
        private SavvyServicesProvider _services;

        public Player(string defaultNameLocalizationName = "Player")
            : base(OpponentType.Player)
        {
            _data = MirraSDK.Player;
            _defaultNameLocalizationKey = defaultNameLocalizationName;
        }

        public void Dispose()
        {
            if (_services is not null)
            {
                _services.Localisation.LocalizationUpdated -= UpdateName;
            }
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Load()
        {
            _services.Localisation.LocalizationUpdated += UpdateName;
            UpdateName();
        }

        private void UpdateName()
        {
            string name = _data.IsLoggedIn
                ? _data.DisplayName
                : _services.Localisation.GetTranslation(_defaultNameLocalizationKey);
            Rename(name);
        }
    }
}