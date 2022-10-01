using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPanelOneTimeOfferMove : UIPanelMove
{
    [SerializeField] private GameObject noAdsPanel;

    #region Injects

    private OneTimeOfferManager _oneTimeOfferManager;
    private PurchaseControl _purchaseControl;

    [Inject]
    private void Construct(OneTimeOfferManager oneTimeOfferManager,
                           PurchaseControl purchaseControl)
    {
        _oneTimeOfferManager = oneTimeOfferManager;
        _purchaseControl = purchaseControl;
    }

    #endregion

    public override void CloseBtn()
    {
        _oneTimeOfferManager.StartTimerPause();
        base.CloseBtn();
    }

    public override void ShowPanel()
    {
        if (_purchaseControl.InterstitialAd && noAdsPanel.activeSelf)
            noAdsPanel.SetActive(false);

        base.ShowPanel();
    }

}
