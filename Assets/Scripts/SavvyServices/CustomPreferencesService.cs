using System.Globalization;
using MirraGames.SDK;
using Savvy.Constants;
using Savvy.Container;
using Savvy.Extensions;
using Savvy.Interfaces;

namespace SavvyServices
{
    public class CustomPreferencesService : NetSavvyResources, IPreferencesService
    {
        private readonly string _settingsPath = $"{PathConstants.SavvyServicesDir}/{nameof(PreferencesSettings)}";
        private PreferencesSettings _settings;

        public void Init() =>
            _settings = LoadResources<PreferencesSettings>(_settingsPath);

        public bool HasKey(string key)
        {
            bool result = MirraSDK.Data.HasKey(key);
            Debug($"Prefs key '{key}' {(result ? "found" : "not found")}", _settings.Debug);

            return result;
        }

        public void SaveInt(string key, int value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            MirraSDK.Data.SetInt(key, value);
            MirraSDK.Data.Save();
            Debug($"Save int prefs. Key '{key}', value '{value}'", _settings.Debug);
        }

        public void SaveFloat(string key, float value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            MirraSDK.Data.SetFloat(key, value);
            MirraSDK.Data.Save();
            Debug($"Save float prefs. Key '{key}', value '{value}'", _settings.Debug);
        }

        public void SaveDouble(string key, double value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            MirraSDK.Data.SetString(key, value.ToString(CultureInfo.InvariantCulture));
            MirraSDK.Data.Save();
            Debug($"Save double prefs. Key '{key}', value '{value}'", _settings.Debug);
        }

        public void SaveString(string key, string value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            MirraSDK.Data.SetString(key, value);
            MirraSDK.Data.Save();
            Debug($"Save string prefs. Key '{key}', value '{value}'", _settings.Debug);
        }

        public void SaveBool(string key, bool value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            MirraSDK.Data.SetBool(key, value);
            MirraSDK.Data.Save();
            Debug($"Save bool prefs. Key '{key}', value '{value}'", _settings.Debug);
        }

        public void SaveEnum<TEnum>(string key, TEnum value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            string result = value.ToString();
            MirraSDK.Data.SetString(key, result);
            MirraSDK.Data.Save();
            Debug($"Save enum prefs. Key '{key}', value '{result}'", _settings.Debug);
        }

        public void SaveJson<TData>(string key, TData value)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            string result = value.ToJson();
            MirraSDK.Data.SetString(key, result);
            MirraSDK.Data.Save();
            Debug($"Save json prefs. Key '{key}', value '{result}'", _settings.Debug);
        }

        public int LoadInt(string key, int defaultValue)
        {
            int result = MirraSDK.Data.GetInt(key, defaultValue);
            Debug($"Load int prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public float LoadFloat(string key, float defaultValue)
        {
            float result = MirraSDK.Data.GetFloat(key, defaultValue);
            Debug($"Load float prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public double LoadDouble(string key, double defaultValue)
        {
            string saved = MirraSDK.Data.GetString(key, defaultValue.ToString(CultureInfo.InvariantCulture));
            double result = double.Parse(saved, CultureInfo.InvariantCulture);
            Debug($"Load double prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public string LoadString(string key, string defaultValue)
        {
            string result = MirraSDK.Data.GetString(key, defaultValue);
            Debug($"Load string prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public bool LoadBool(string key, bool defaultValue)
        {
            bool result = MirraSDK.Data.GetBool(key, defaultValue);
            Debug($"Load bool prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public TEnum LoadEnum<TEnum>(string key, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum result = MirraSDK.Data.GetString(key).ToEnumOrDefault(defaultValue);
            Debug($"Load enum prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public TData LoadJson<TData>(string key, TData defaultValue)
        {
            TData result = MirraSDK.Data.GetString(key).FromJson<TData>();
            Debug($"Load json prefs. Key '{key}', value '{result}'", _settings.Debug);

            return result;
        }

        public void DeleteKey(string key)
        {
            if (IsNullOrEmpty(key))
            {
                return;
            }

            MirraSDK.Data.DeleteKey(key);
            MirraSDK.Data.Save();
            Debug($"Delete prefs key '{key}'", _settings.Debug);
        }

        public void DeleteAll()
        {
            MirraSDK.Data.DeleteAll();
            MirraSDK.Data.Save();
            Debug("Delete all prefs keys", _settings.Debug);
        }

        private bool IsNullOrEmpty(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                Error("Save key cannot be null or empty");
                return true;
            }

            return false;
        }
    }
}