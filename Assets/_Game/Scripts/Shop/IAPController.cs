using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IAPController : MonoBehaviour
{
    [Header("JunkBotSet")]
    [SerializeField] private GameObject junkBotSetPanel;
    [SerializeField] private Button junkBotSetButton;
    [SerializeField] private GameObject junkBotSetNoAds;

    [Header("Every Day")]
    [SerializeField] private EveryDay everyDay;

    [Header("Special Offer")]
    [SerializeField] private Button specialOfferPanel;
    [SerializeField] private GameObject specialOfferNoAds;

    [Header("Boosters")]
    [SerializeField] private GameObject bostersForeverPanel;
    [SerializeField] private Button crusherButton;
    [SerializeField] private Button forceButton;
    [SerializeField] private Button trailerButton;
    [SerializeField] private Button magnetButton;
    [SerializeField] private RectTransform localOnlyBoosterPanle;
    [SerializeField] private Button trueNoAdsButton;

    #region Injects

    private ScoreManager _scoreManager;
    private SettingsShop _settingsShop;
    private PurchaseControl _purchaseControl;
    private AnalyticsPurchase _analyticsPurchase;
    // private UIPanelOneTimeOfferMove _uiPanelOneTimeOfferMove;

    [Inject]
    private void Construct(ScoreManager scoreManager,
                           SettingsShop settingsShop,
                           PurchaseControl purchaseControl,
                           AnalyticsPurchase analyticsPurchase
                           /*UIPanelOneTimeOfferMove uiPanelOneTimeOfferMove*/)
    {
        _scoreManager = scoreManager;
        _settingsShop = settingsShop;
        _purchaseControl = purchaseControl;
        _analyticsPurchase = analyticsPurchase;
        // _uiPanelOneTimeOfferMove = uiPanelOneTimeOfferMove;
    }

    #endregion     

    private void OnEnable()
    {
        junkBotSetPanel.gameObject.SetActive(!_purchaseControl.JunkBotSet);
        junkBotSetNoAds.SetActive(!_purchaseControl.InterstitialAd);


        specialOfferPanel.gameObject.SetActive(!_purchaseControl.SpecialOffer);
        specialOfferNoAds.SetActive(!_purchaseControl.InterstitialAd);


        bostersForeverPanel.SetActive(!_purchaseControl.NoAdsBoost);

        if (bostersForeverPanel.activeSelf)
        {
            crusherButton.gameObject.SetActive(!_purchaseControl.CrusherBoost);
            forceButton.gameObject.SetActive(!_purchaseControl.ForceBoost);
            trailerButton.gameObject.SetActive(!_purchaseControl.TrailerBoost);
            magnetButton.gameObject.SetActive(!_purchaseControl.MagnetBoost);

            if (!crusherButton.gameObject.activeSelf &&
               !forceButton.gameObject.activeSelf &&
               !trailerButton.gameObject.activeSelf &&
               !magnetButton.gameObject.activeSelf)
            {
                localOnlyBoosterPanle.gameObject.SetActive(false);
            }
        }
    }

    public void OnPurchaseJunkBotSetCompleted(string product_id, string transactionID)
    {
        _scoreManager.TotalHard += _settingsShop.JunkBotCountCrystals;
        _purchaseControl.JunkBotSet = true;

        if (junkBotSetButton)
            junkBotSetButton.interactable = false;

        _analyticsPurchase.SetEvent(_settingsShop.JunkBotCostMoney, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();
    }

    public void OnEveryDay()
    {
        _scoreManager.TotalHard += _settingsShop.EveryDayCrystals;
        everyDay.Click();
    }

    public void OnPurchaseSpecialOfferCompleted(string product_id, string transactionID)
    {
        _purchaseControl.SpecialOffer = true;

        if (specialOfferPanel)
            specialOfferPanel.interactable = false;

        _scoreManager.TotalHard += _settingsShop.SpecialOfferCristals;
        _scoreManager.TmpMoney += _settingsShop.SpecialOfferCoins;

        _analyticsPurchase.SetEvent(_settingsShop.SpecialOfferCostMoney, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();
    }



    public void OnPurchaseSmallCoin()
    {
        if (_scoreManager.TotalHard < _settingsShop.SmallCoinsCost) return;

        _scoreManager.TotalHard -= _settingsShop.SmallCoinsCost;
        _scoreManager.TmpMoney += _settingsShop.SmallCoins;
    }

    public void OnPurchaseMiddleCoin()
    {
        if (_scoreManager.TotalHard < _settingsShop.MiddleCoinsCost) return;

        _scoreManager.TotalHard -= _settingsShop.MiddleCoinsCost;
        _scoreManager.TmpMoney += _settingsShop.MiddleCoins;
    }

    public void OnPurchaseBigCoin()
    {
        if (_scoreManager.TotalHard < _settingsShop.BigCoinsCost) return;

        _scoreManager.TotalHard -= _settingsShop.BigCoinsCost;
        _scoreManager.TmpMoney += _settingsShop.BigCoins;
    }

    public void OnPurchaseSmallHard(string product_id, string transactionID)
    {
        _scoreManager.TotalHard += _settingsShop.SmallCrystals;

        _analyticsPurchase.SetEvent(_settingsShop.SmallCrystalsCostMoney, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();
    }

    public void OnPurchaseMiddleHard(string product_id, string transactionID)
    {
        _scoreManager.TotalHard += _settingsShop.MiddleCrystals;

        _analyticsPurchase.SetEvent(_settingsShop.MiddleCrystalsCostMoney, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();
    }

    public void OnPurchaseBigHard(string product_id, string transactionID)
    {
        _scoreManager.TotalHard += _settingsShop.BigCrystals;

        _analyticsPurchase.SetEvent(_settingsShop.BigCrystalsCostMoney, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();
    }

    public void OnPurchaseCrusherBoost(string product_id, string transactionID)
    {
        if (!_purchaseControl.CrusherBoost)
        {
            if (crusherButton)
                crusherButton.interactable = false;

            _purchaseControl.CrusherBoost = true;

            _analyticsPurchase.SetEvent(_settingsShop.BoostCrusherCostMoney, product_id, transactionID);
            _purchaseControl.SetAnalyticsFirstPurchse();
        }
    }

    public void OnPurchaseCrusherBoost()
    {
        if (!_purchaseControl.CrusherBoost)
        {
            if (crusherButton)
                crusherButton.interactable = false;
            _purchaseControl.CrusherBoost = true;
        }
    }

    public void OnPurchaseForceBoost(string product_id, string transactionID)
    {
        if (!_purchaseControl.ForceBoost)
        {
            if (forceButton)
                forceButton.interactable = false;
            _purchaseControl.ForceBoost = true;

            _analyticsPurchase.SetEvent(_settingsShop.BoostForceCostMoney, product_id, transactionID);
            _purchaseControl.SetAnalyticsFirstPurchse();
        }
    }

    public void OnPurchaseForceBoost()
    {
        if (!_purchaseControl.ForceBoost)
        {
            if (forceButton)
                forceButton.interactable = false;
            _purchaseControl.ForceBoost = true;
        }
    }

    public void OnPurchaseTrailerBoost(string product_id, string transactionID)
    {
        if (!_purchaseControl.TrailerBoost)
        {
            if (trailerButton)
                trailerButton.interactable = false;
            _purchaseControl.TrailerBoost = true;

            _analyticsPurchase.SetEvent(_settingsShop.BoostTrailerCostMoney, product_id, transactionID);
            _purchaseControl.SetAnalyticsFirstPurchse();
        }
    }

    public void OnPurchaseTrailerBoost()
    {
        if (!_purchaseControl.TrailerBoost)
        {
            if (trailerButton)
                trailerButton.interactable = false;

            _purchaseControl.TrailerBoost = true;
        }
    }

    public void OnPurchaseMagnetBoost(string product_id, string transactionID)
    {
        if (!_purchaseControl.MagnetBoost)
        {
            if (magnetButton)
                magnetButton.interactable = false;

            _purchaseControl.MagnetBoost = true;

            _analyticsPurchase.SetEvent(_settingsShop.BoostMagnetCostMoney, product_id, transactionID);
            _purchaseControl.SetAnalyticsFirstPurchse();
        }
    }

    public void OnPurchaseMagnetBoost()
    {
        if (!_purchaseControl.MagnetBoost)
        {
            if (magnetButton)
                magnetButton.interactable = false;
            _purchaseControl.MagnetBoost = true;
        }
    }

    public void OnPurchaseTruNoAds(string product_id, string transactionID)
    {
        if (trueNoAdsButton)
            trueNoAdsButton.interactable = false;

        _purchaseControl.NoAdsBoost = true;

        OnPurchaseCrusherBoost();
        OnPurchaseForceBoost();
        OnPurchaseTrailerBoost();
        OnPurchaseMagnetBoost();

        _analyticsPurchase.SetEvent(_settingsShop.TrueNoAdsCostMoney, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();
    }

    public void OnPurchaseFailed()
    {
        Debug.Log("Purchase Canceled");
    }
}
