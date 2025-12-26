using Savvy.Bootstrap;
using Savvy.Interfaces;
using SavvyServices;

namespace Bootstrap
{
    internal class ProjectBehaviour : ProjectBehaviourBase
    {
        protected override void OnProjectBehaviourInitialized()
        { }

        protected override void RegisterLocalizationService() =>
            RegisterService<ILocalizationService>(new CustomLocalizationService());

        protected override void RegisterMediationService() =>
            RegisterService<IMediationService>(new CustomMediationService());

        protected override void RegisterPreferencesService() =>
            RegisterService<IPreferencesService>(new CustomPreferencesService());
    }
}