using Savvy.Container;
using Savvy.Interfaces;

namespace Bootstrap
{
    public class SavvyServicesProvider : NetSavvy
    {
        private ICoroutineRunnerService _coroutineRunner;
        private ILocalizationService _localization;
        private IMediationService _mediation;
        private IPreferencesService _preferences;
        private ISceneLoaderService _sceneLoader;

        public ICoroutineRunnerService CoroutineRunner =>
            _coroutineRunner ??= GetService<ICoroutineRunnerService>();

        public ILocalizationService Localisation =>
            _localization ??= GetService<ILocalizationService>();

        public IMediationService Mediation =>
            _mediation ??= GetService<IMediationService>();

        public IPreferencesService Preferences =>
            _preferences ??= GetService<IPreferencesService>();

        public ISceneLoaderService SceneLoader =>
            _sceneLoader ??= GetService<ISceneLoaderService>();
    }
}