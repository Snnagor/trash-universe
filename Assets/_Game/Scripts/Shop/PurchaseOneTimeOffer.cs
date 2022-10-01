using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PurchaseOneTimeOffer : MonoBehaviour
{

    [SerializeField] private UIPanelOneTimeOfferMove _uiPanelOneTimeOfferMove;

    #region Injects

    private ScoreManager _scoreManager;
    private SettingsShop _settingsShop;
    private PurchaseControl _purchaseControl;
    private AnalyticsPurchase _analyticsPurchase;

    [Inject]
    private void Construct(ScoreManager scoreManager,
                           SettingsShop settingsShop,
                           PurchaseControl purchaseControl,
                           AnalyticsPurchase analyticsPurchase)
    {
        _scoreManager = scoreManager;
        _settingsShop = settingsShop;
        _purchaseControl = purchaseControl;
        _analyticsPurchase = analyticsPurchase;
    }

    #endregion     

    public void OnPurchaseOneTimeOfferCompleted(string product_id, string transactionID)
    {
       
        _scoreManager.TotalHard += _settingsShop.CountCrystalsOneTimeOffer;
        _scoreManager.TmpMoney += _settingsShop.CountCoinOneTimeOffer;
        _purchaseControl.OneTimeOffer = true;
        _uiPanelOneTimeOfferMove.HidePanel();        
        _analyticsPurchase.SetEvent(_settingsShop.CostOneTimeOffer, product_id, transactionID);
        _purchaseControl.SetAnalyticsFirstPurchse();

    }
}
