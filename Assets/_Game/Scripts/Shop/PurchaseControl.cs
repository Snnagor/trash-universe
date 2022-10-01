using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PurchaseControl
{
    public bool AnalyticsFirstPurchase { get; set; }

    public bool InterstitialAd { get; set; }
    public bool RewardedAd { get; set; }


    private bool junkBotSet;
    public bool JunkBotSet 
    {
        get => junkBotSet;

        set
        {
            if (junkBotSet.Equals(value)) return;

            junkBotSet = value;
            SetForJunkBotSet();            
        }    
    }


    private bool specialOffer;
    public bool SpecialOffer
    {
        get => specialOffer;

        set
        {
            if (specialOffer.Equals(value)) return;
            specialOffer = value;

            SetSpecialOffer();
        }
    }


    private bool oneTimeOffer;
    public bool OneTimeOffer
    {
        get => oneTimeOffer;

        set
        {
            if (oneTimeOffer.Equals(value)) return;

            oneTimeOffer = value;
            
            SetSpecialOffer();
        }
    }


    private bool forceBoost;
    public bool ForceBoost
    {
        get => forceBoost;

        set
        {
            if (forceBoost.Equals(value)) return;

            forceBoost = value;
        }
    }


    private bool crusherBoost;
    public bool CrusherBoost
    {
        get => crusherBoost;

        set
        {
            if (crusherBoost.Equals(value)) return;

            crusherBoost = value;
        }
    }


    private bool trailerBoost;
    public bool TrailerBoost
    {
        get => trailerBoost;

        set
        {
            if (trailerBoost.Equals(value)) return;

            trailerBoost = value;            
        }
    }


    private bool magnetBoost;
    public bool MagnetBoost
    {
        get => magnetBoost;

        set
        {
            if (magnetBoost.Equals(value)) return;

            magnetBoost = value;            
        }
    }


    private bool noAdsBoost;
    public bool NoAdsBoost
    {
        get => noAdsBoost;

        set
        {
            if (noAdsBoost.Equals(value)) return;

            noAdsBoost = value;
            SetNoAdsBoost();
        }
    }


    #region Injects

    private SettingsShop _settingsShop;
    private PlayerMove _playerMove;
    private ClothesManager _clothesManager;
    private BtnContoller _btnController;
    private AnalyticsFirstPurchase _analyticsFirstPurchase;

    [Inject]
    private void Construct(SettingsShop settingsShop,
                           PlayerMove playerMove,
                           ClothesManager clothesManager,
                           BtnContoller btnController,
                           AnalyticsFirstPurchase analyticsFirstPurchase)
    {       
        _settingsShop = settingsShop;
        _playerMove = playerMove;
        _clothesManager = clothesManager;
        _btnController = btnController;
        _analyticsFirstPurchase = analyticsFirstPurchase;
    }

    #endregion
    

    private void SetForJunkBotSet()
    {
        ChangeSpeedJunkBotSet();

        _clothesManager.SetHadphones();
        InterstitialAd = true;
        _btnController.DisableIconNoAds();
    }

    private void ChangeSpeedJunkBotSet()
    {
        if (junkBotSet)
        {
            _playerMove.SetDeltaSpeed();
        }
    }

    private void SetSpecialOffer()
    {        
        if (!InterstitialAd)
        {           
            InterstitialAd = true;            
            _btnController.DisableIconNoAds();            
        }        
    }

    private void SetNoAdsBoost()
    {
        InterstitialAd = true;
        RewardedAd = true;

        _btnController.DisableIconNoAds();
    }

    public void SetAnalyticsFirstPurchse()
    {
        if (!AnalyticsFirstPurchase)
        {
            AnalyticsFirstPurchase = true;
            _analyticsFirstPurchase.SetEvent();
        }
    }
}
