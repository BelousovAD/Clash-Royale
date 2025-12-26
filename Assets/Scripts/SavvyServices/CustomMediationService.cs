using System;
using MirraGames.SDK;
using Savvy.Constants;
using Savvy.Container;
using Savvy.Extensions;
using Savvy.Interfaces;
using UnityEngine;

namespace SavvyServices
{
    public class CustomMediationService : NetSavvyResources, IMediationService, IFixedTickable, IDisposable
    {
        private readonly string _settingsPath = $"{PathConstants.SavvyServicesDir}/{nameof(MediationSettings)}";

        private IEventBus _eventBus;
        private IPreferencesService _preferences;
        private IGameObjectFactory _gameObjectFactory;
        private IMediationNetwork _mediationNetwork;
        private MediationSettings _settings;
        private int _bannerAdCount;
        private int _interstitialAdCount;
        private int _rewardedAdCount;
        private Action _interstitialAdClosed;
        private Action _rewardedAdCompleted;
        private Action _rewardedAdClosed;
        private bool _isRewardedComplete;
        private bool _isNoAds;
        private float _deltaTimer;
        private int _interstitialInterval;
        private GameObject _interstitialAdCountdown;

        public event Action BannerAdLoaded;
        public event Action<bool> InterstitialAdLoaded;
        public event Action<bool> RewardedAdLoaded;
        public event Action<int> InterstitialAdTimerUpdate;

        public bool BannerAdIsReady => _mediationNetwork.BannerAdIsReady;

        public bool InterstitialAdIsReady => _mediationNetwork.InterstitialAdIsReady;

        public bool RewardedAdIsReady => _mediationNetwork.RewardedAdIsReady;

        public void Inject()
        {
            _eventBus = GetService<IEventBus>();
            _preferences = GetService<IPreferencesService>();
            _gameObjectFactory = GetService<IGameObjectFactory>();
        }

        public void Init()
        {
            _settings = LoadResources<MediationSettings>(_settingsPath);
            _mediationNetwork = new MirraAdapter(_settings);

            MirraSDK.WaitForProviders(() =>
            {
                _bannerAdCount = _preferences.LoadInt(GetBannerAdKey());
                _interstitialAdCount = _preferences.LoadInt(GetInterstitialAdKey());
                _rewardedAdCount = _preferences.LoadInt(GetRewardedAdKey());
                _isNoAds = _preferences.LoadBool(GetNoAdsKey());
            });

            if (_settings.AutoShowInterstitialAd && _settings.InterstitialAdCountdownPrefab == false)
            {
                Error("InterstitialAdCountdown prefab is not set");
            }

            ResetInterstitialInterval();
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
            _mediationNetwork.Dispose();
        }

        public void FixedTick() =>
            AutoInterstitialAdUpdate();

        public void EnableNoAds()
        {
            _isNoAds = true;
            _preferences.SaveBool(GetNoAdsKey(), _isNoAds);
            Debug("No Ads enabled", _settings.Debug);
        }

        public void DisableNoAds()
        {
            _isNoAds = false;
            _preferences.SaveBool(GetNoAdsKey(), _isNoAds);
            Debug("No Ads disabled", _settings.Debug);
        }

        public void ShowBannerAd()
        {
            if (_isNoAds)
            {
                Debug("Show banner Ad cancelled. NoAds is enabled", _settings.Debug);
                return;
            }

            if (BannerAdIsReady)
            {
                _mediationNetwork.ShowBannerAd();
                Debug("Show banner Ad", _settings.Debug);
            }
            else
            {
                Error("Banner Ad are not ready");
            }
        }

        public void HideBannerAd()
        {
            _mediationNetwork.HideBannerAd();
            Debug("Hide banner Ad", _settings.Debug);
        }

        public void ShowInterstitialAd(Action onClosed, string placement)
        {
            if (_isNoAds)
            {
                Debug("Show interstitial Ad cancelled. NoAds is enabled", _settings.Debug);
                return;
            }

            if (InterstitialAdIsReady)
            {
                _interstitialAdClosed = onClosed;
                placement = $"{placement}_Interstitial";
                Debug($"Show interstitial Ad, source '{placement}'", _settings.Debug);
                _mediationNetwork.ShowInterstitialAd(placement);
            }
            else
            {
                Error("Interstitial Ad are not ready");
            }
        }

        public void ShowRewardedAd(Action onCompleted, Action onClosed, string placement)
        {
            if (RewardedAdIsReady)
            {
                _rewardedAdCompleted = onCompleted;
                _rewardedAdClosed = onClosed;
                _isRewardedComplete = false;
                placement = $"{placement}_Rewarded";
                Debug($"Show rewarded Ad, source '{placement}'", _settings.Debug);
                _mediationNetwork.ShowRewardedAd(placement);
            }
            else
            {
                Error("Rewarded Ad are not ready");
            }
        }

        private void Subscribe()
        {
            _eventBus.Subscribe<AdEvent>(HandleAdsEvent);
            _eventBus.Subscribe<AdRevenueData>(HandleAdRevenue);
        }

        private void Unsubscribe()
        {
            _eventBus.Unsubscribe<AdEvent>(HandleAdsEvent);
            _eventBus.Unsubscribe<AdRevenueData>(HandleAdRevenue);
        }

        private void HandleAdsEvent(AdEvent adsEvent)
        {
            switch (adsEvent)
            {
                case AdEvent.BannerAdLoaded:
                    Debug("Banner Ad loaded", _settings.Debug);
                    BannerAdLoaded?.Invoke();
                    break;
                case AdEvent.BannerAdError:
                    Error("Banner Ad error, enable 'Debug'");
                    break;
                case AdEvent.InterstitialAdLoaded:
                    Debug("Interstitial Ad loaded", _settings.Debug);
                    InterstitialAdLoaded?.Invoke(true);
                    break;
                case AdEvent.InterstitialAdOpened:
                    Debug("Interstitial Ad opened", _settings.Debug);
                    InterstitialAdLoaded?.Invoke(false);
                    break;
                case AdEvent.InterstitialAdClosed:
                    Debug("Interstitial Ad closed", _settings.Debug);
                    ResetInterstitialInterval();
                    _interstitialAdClosed?.Invoke();
                    break;
                case AdEvent.InterstitialAdError:
                    Error("Interstitial Ad error, enable 'Debug'");
                    break;
                case AdEvent.RewardedAdLoaded:
                    Debug("Rewarded Ad loaded", _settings.Debug);
                    RewardedAdLoaded?.Invoke(true);
                    break;
                case AdEvent.RewardedAdOpened:
                    Debug("Rewarded Ad opened", _settings.Debug);
                    RewardedAdLoaded?.Invoke(false);
                    break;
                case AdEvent.RewardedAdCompleted:
                    Debug("Rewarded Ad completed", _settings.Debug);
                    _isRewardedComplete = true;
                    _rewardedAdCompleted?.Invoke();
                    break;
                case AdEvent.RewardedAdClosed:
                    if (!_isRewardedComplete)
                    {
                        Debug("The user has not earned the reward", _settings.Debug);
                        _rewardedAdClosed?.Invoke();
                    }

                    Debug("Rewarded Ad closed", _settings.Debug);
                    break;
                case AdEvent.RewardedAdError:
                    Error("Rewarded Ad error, enable 'Debug'");
                    break;
                default:
                    adsEvent.ToEnumUnknown();
                    break;
            }
        }

        private void HandleAdRevenue(AdRevenueData revenueData) =>
            SendAdCount(revenueData.AdType.ToEnumOrDefault<AdType>());

        private void SendAdCount(AdType adType)
        {
            switch (adType)
            {
                case AdType.Banner:
                    if (_settings.SendBannerAdCount)
                    {
                        _bannerAdCount++;
                        _preferences.SaveInt(GetBannerAdKey(), _bannerAdCount);
                        _eventBus.Invoke(new BannerAdData { Count = _bannerAdCount });
                    }

                    break;
                case AdType.Interstitial:
                    if (_settings.SendInterstitialAdCount)
                    {
                        _interstitialAdCount++;
                        _preferences.SaveInt(GetInterstitialAdKey(), _interstitialAdCount);
                        _eventBus.Invoke(new InterstitialAdData { Count = _interstitialAdCount });
                    }

                    break;
                case AdType.Rewarded:
                    if (_settings.SendRewardedAdCount)
                    {
                        _rewardedAdCount++;
                        _preferences.SaveInt(GetRewardedAdKey(), _rewardedAdCount);
                        _eventBus.Invoke(new RewardedAdData { Count = _rewardedAdCount });
                    }

                    break;
                default:
                    adType.ToEnumUnknown();
                    break;
            }
        }

        private void AutoInterstitialAdUpdate()
        {
            if (_isNoAds || _settings.AutoShowInterstitialAd == false)
            {
                return;
            }

            _deltaTimer += Time.fixedDeltaTime;

            if (_deltaTimer >= 1f)
            {
                _deltaTimer -= 1f;
                _interstitialInterval--;

                if (_interstitialInterval <= _settings.InterstitialAdCheckTimer && InterstitialAdIsReady == false)
                {
                    _interstitialInterval += _settings.InterstitialAdCheckTimer;
                }

                if (_interstitialInterval <= _settings.InterstitialAdCountdownTimer)
                {
                    if (_interstitialAdCountdown is null)
                    {
                        Debug("Create interstitial ad warning object", _settings.Debug);
                        _interstitialAdCountdown =
                            _gameObjectFactory.Instantiate(_settings.InterstitialAdCountdownPrefab);
                    }

                    InterstitialAdTimerUpdate?.Invoke(_interstitialInterval);

                    if (_interstitialInterval == 0)
                    {
                        _gameObjectFactory.Destroy(_interstitialAdCountdown);
                        ResetInterstitialInterval();
                        ShowInterstitialAd(null, nameof(_settings.AutoShowInterstitialAd));
                    }
                }
            }
        }

        private void ResetInterstitialInterval() =>
            _interstitialInterval = _settings.InterstitialAdInterval;

        private string GetBannerAdKey() =>
            GetPrefsKey(nameof(_bannerAdCount));

        private string GetInterstitialAdKey() =>
            GetPrefsKey(nameof(_interstitialAdCount));

        private string GetRewardedAdKey() =>
            GetPrefsKey(nameof(_rewardedAdCount));

        private string GetNoAdsKey() =>
            GetPrefsKey(nameof(_isNoAds));

        private string GetPrefsKey(string name) =>
            _settings.PrefsKey + name;
    }
}