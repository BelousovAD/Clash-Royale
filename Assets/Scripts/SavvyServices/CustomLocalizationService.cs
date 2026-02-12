using System;
using System.Collections.Generic;
using MirraGames.SDK;
using MirraGames.SDK.Common;
using Savvy.Constants;
using Savvy.Container;
using Savvy.Extensions;
using Savvy.Interfaces;
using UnityEngine;

namespace SavvyServices
{
    public class CustomLocalizationService : NetSavvyResources, ILocalizationService
    {
        private readonly string _settingsPath = $"{PathConstants.SavvyServicesDir}/{nameof(LocalizationSettings)}";
        private readonly Dictionary<string, string> _dictionary = new ();
        private LocalizationSettings _settings;
        private IPreferencesService _preferences;
        private SystemLanguage? _current;

        public event Action LocalizationUpdated;

        public void Inject() =>
            _preferences = GetService<IPreferencesService>();

        public void Init()
        {
            _settings = LoadResources<LocalizationSettings>(_settingsPath);
            
            MirraSDK.WaitForProviders(() =>
            {
                LoadTextAsset(GetTextAsset(GetLanguage()));
            });
        }

        public SystemLanguage GetLanguage()
        {
            if (_current is null)
            {
                LanguageType language = MirraSDK.Language.Current;
                _current = language.ToEnumOrDefault(SystemLanguage.English);
            }

            return (SystemLanguage)_current;
        }

        public void SetLanguage(SystemLanguage language)
        {
            _current = language;
            LoadTextAsset(GetTextAsset(language));
        }

        public string GetTranslation(string key) =>
            _dictionary.ContainsKey(key) ? _dictionary[key] : $"{{{key}}}";

        public string GetTranslation(string key, params object[] args) =>
            _dictionary.ContainsKey(key) ? string.Format(_dictionary[key], args) : $"{{{key}}}";

        private TextAsset GetTextAsset(SystemLanguage language)
        {
            foreach (TranslationsData translation in _settings.TranslationsData)
            {
                if (translation.SystemLanguage == language)
                {
                    return GetAvailableLanguage(translation.SystemLanguage, translation.TextAsset);
                }
            }

            return GetAvailableLanguage(_settings.DefaultSystemLanguage, _settings.DefaultTextAsset);
        }

        private TextAsset GetAvailableLanguage(SystemLanguage systemLanguage, TextAsset language)
        {
            if (language is null)
            {
                _preferences.SaveEnum(GetPrefsKey(), _settings.DefaultSystemLanguage);

                return _settings.DefaultTextAsset;
            }

            _preferences.SaveEnum(GetPrefsKey(), systemLanguage);

            return language;
        }

        private void LoadTextAsset(TextAsset language)
        {
            try
            {
                LocalizationDictionaryData json = language.text.FromJson<LocalizationDictionaryData>();
                _dictionary.Clear();

                foreach (LocalizationData localization in json.Localization)
                {
                    _dictionary.Add(localization.Key, localization.Value);
                }

                LocalizationUpdated?.Invoke();
                Debug($"Load localization '{language.name}'", _settings.Debug);
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }

        private string GetPrefsKey() =>
            _settings.PrefsKey;
    }
}