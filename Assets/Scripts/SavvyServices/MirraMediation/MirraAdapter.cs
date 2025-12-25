namespace SavvyServices.MirraMediation
{
    using MirraGames.SDK;
    using Savvy.Container;
    using Savvy.Interfaces;

    internal class MirraAdapter : NetSavvy, IMediationNetwork
    {
        private readonly IEventBus _eventBus;

        public MirraAdapter(MediationSettings settings)
        {
            _eventBus = GetService<IEventBus>();
            Debug("MirraSDK Network successfully initialized", settings.Debug);
        }

        public bool BannerAdIsReady => MirraSDK.Ads.IsBannerReady;
        
        public bool InterstitialAdIsReady => MirraSDK.Ads.IsInterstitialReady;
        
        public bool RewardedAdIsReady => MirraSDK.Ads.IsRewardedReady;

        public void Dispose()
        { }

        public void ShowBannerAd() => 
            MirraSDK.Ads.InvokeBanner();

        public void HideBannerAd() => 
            MirraSDK.Ads.DisableBanner();

        public void ShowInterstitialAd(string placement) =>
            MirraSDK.Ads.InvokeInterstitial(
                () => _eventBus.Invoke(AdEvent.InterstitialAdOpened),
                _ => _eventBus.Invoke(AdEvent.InterstitialAdClosed));

        public void ShowRewardedAd(string placement) =>
            MirraSDK.Ads.InvokeRewarded(
                () => _eventBus.Invoke(AdEvent.RewardedAdOpened),
                isSuccess => _eventBus.Invoke(isSuccess ? AdEvent.RewardedAdCompleted : AdEvent.RewardedAdClosed));
    }
}