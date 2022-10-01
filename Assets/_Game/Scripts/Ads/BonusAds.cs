using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusAds : RewardedAd
{   
    #region Injects

    private BonusesManager _bonusesManager;
    private AnalyticsRewardeAd _analyticsRewardeAd;

    [Inject] 
    private void Construct(BonusesManager bonusesManager,
                           AnalyticsRewardeAd analyticsRewardeAd)
    {
        _bonusesManager = bonusesManager;
        _analyticsRewardeAd = analyticsRewardeAd;
    }

    #endregion

    public override void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo)
    {        
        if (_bonusesManager == null) return;

        _bonusesManager.InitActiveBonus();

        _analyticsRewardeAd.SetEvent("reward_boost");
    }

}
