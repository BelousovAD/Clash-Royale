namespace Mediation
{
    using Bootstrap;
    using Common;
    using Reflex.Attributes;
    using UnityEngine;
    using UnityEngine.UI;

    internal class AdvertisementButton : AbstractButton
    {
        [SerializeField] private Button _buttonToComplete;
        [SerializeField] private bool _isRewarded;
        
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        protected override void HandleClick()
        {
            HandleComplete();
            return;
            
            if (_isRewarded)
            {
                _services.Mediation.ShowRewardedAd(HandleComplete);
            }
            else
            {
                _services.Mediation.ShowInterstitialAd(HandleComplete);
            }
        }

        private void HandleComplete() =>
            _buttonToComplete.onClick.Invoke();
    }
}