using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OneTimeOfferManager : MonoBehaviour
{
    [SerializeField] private UIPanelOneTimeOfferMove uiPanelOneTimeOffer;

    private float currentTimePause = -2;

    #region Injects

    private SettingsShop _settingsShop;
    private PurchaseControl _purchaseControl;

    [Inject]
    private void Construct(SettingsShop settingsShop,
                           PurchaseControl purchaseControl)
    {
        _settingsShop = settingsShop;
        _purchaseControl = purchaseControl;
    }

    #endregion

    void Start()
    {
        StartTimerPause();
    }

    public void StartTimerPause()
    {
        if (!_purchaseControl.OneTimeOffer)
            currentTimePause = _settingsShop.TimeBetweenOffers;
    }

    void Update()
    {
        if (currentTimePause > 0)
        {
            currentTimePause -= Time.deltaTime;
        }
        else if (currentTimePause > -1)
        {
            uiPanelOneTimeOffer.ShowPanel();
            currentTimePause = -2;
        }
    }


}
