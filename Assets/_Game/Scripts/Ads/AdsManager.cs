using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private BonusAds bonusAd;
    [SerializeField] private InterstitialsAd interstitialsAd;
    [SerializeField] private BannersAd bannersAd;

    private float currentTimeShowInterstitialAd = -2;

    #region Injects

    private Settings _settings;
    private PurchaseControl _purchaseControl;

    [Inject]
    private void Construct(Settings settings,
                           PurchaseControl purchaseControl)
    {
        _settings = settings;
        _purchaseControl = purchaseControl;
    }

    #endregion

    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
        };

        MaxSdk.SetSdkKey("vSfzs3bbK48C3itHm2HQ3wM2eeULfzTpnsLSzE-c72-RaKkERIR5oKkV6ezlHpPw1Ga80lRk9MDrgm3__Nw2T0");
        // MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();

        if (!_purchaseControl.RewardedAd)
            bonusAd.InitializeRewardedAds();

        if (!_purchaseControl.InterstitialAd)
            interstitialsAd.InitializeInterstitialAds();
        // bannersAd.InitializeBannerAds();

        if (!_purchaseControl.InterstitialAd)
            currentTimeShowInterstitialAd = _settings.TimeFirstShowAd;
    }

    public void ShowInterstitialAD()
    {
        interstitialsAd.ShowInterstitialAD();
    }

    public void ShowRewardedAd()
    {
        bonusAd.ShowRewardedAd();
    }
    

    private void Update()
    {
        TimerInterstitialAd();
    }

    private void TimerInterstitialAd()
    {
        if (!_purchaseControl.InterstitialAd)
        {
            if (currentTimeShowInterstitialAd > 0)
            {
                currentTimeShowInterstitialAd -= Time.deltaTime;
            }
            else if (currentTimeShowInterstitialAd > -1)
            {
                ShowInterstitialAD();
                currentTimeShowInterstitialAd = _settings.DeltaTimeSeconShowAd;
            }
        }
    }
}
