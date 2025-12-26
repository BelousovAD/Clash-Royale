using Bootstrap;
using Common;
using Reflex.Attributes;

namespace Save
{
    internal class ResetDataButton : AbstractButton
    {
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        protected override void HandleClick() =>
            _services.Preferences.DeleteAll();
    }
}