using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AnalyticsRewardeAd : MonoBehaviour
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

    public void SetEvent(string placement)
    {
        AdjustEvent adjustEvent = new AdjustEvent("3qple8");
        adjustEvent.setCallbackId("rv_finish");
        adjustEvent.addCallbackParameter("placement", placement);
        adjustEvent.addCallbackParameter("time", _analyticsTimerService.CurrentMinutTime.ToString());
        adjustEvent.addCallbackParameter("level", SceneManager.GetActiveScene().buildIndex.ToString());
        adjustEvent.addCallbackParameter("place_name", $"Platform {_gameManager.AnalyticsCountPlatform}");

        Adjust.trackEvent(adjustEvent);
    }
}
