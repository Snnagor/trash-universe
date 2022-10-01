using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AnalyticsPurchase : MonoBehaviour
{
    #region Injects

    private AnalyticsTimerService _analyticsTimerService;
    private GameManager _gameManager;

    [Inject]
    private void Construct(AnalyticsTimerService analyticsTimerService,
                           GameManager gameManager)
    {
        _analyticsTimerService = analyticsTimerService;
        _gameManager = gameManager;
    }

    #endregion

    public void SetEvent(double revenue, string product_id, string transactionID)
    {
        AdjustEvent adjustEvent = new AdjustEvent("x89rau");        
        adjustEvent.setRevenue(revenue, "USS");
        adjustEvent.setTransactionId(transactionID);
        adjustEvent.setCallbackId("purchase");
        adjustEvent.addCallbackParameter("product_id", product_id);
        adjustEvent.addCallbackParameter("time", _analyticsTimerService.CurrentMinutTime.ToString());
        adjustEvent.addCallbackParameter("level", SceneManager.GetActiveScene().buildIndex.ToString());
        adjustEvent.addCallbackParameter("place_name", $"Platform {_gameManager.AnalyticsCountPlatform}");
        
        Adjust.trackEvent(adjustEvent);
    }
}
