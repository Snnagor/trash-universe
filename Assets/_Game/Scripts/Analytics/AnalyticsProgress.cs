using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AnalyticsProgress : MonoBehaviour
{
    #region Injects

    private AnalyticsTimerService _analyticsTimerService;

    [Inject]
    private void Construct(AnalyticsTimerService analyticsTimerService)
    {
        _analyticsTimerService = analyticsTimerService;
    }

    #endregion

    public void SetEvent(int currentPlatform)
    {
        AdjustEvent adjustEvent = new AdjustEvent("r5ja3k");
        adjustEvent.setCallbackId("progress");
        adjustEvent.addCallbackParameter("level", SceneManager.GetActiveScene().buildIndex.ToString());
        adjustEvent.addCallbackParameter("place_name", $"Platform {currentPlatform}");
        adjustEvent.addCallbackParameter("time", _analyticsTimerService.CurrentMinutTime.ToString());
        Adjust.trackEvent(adjustEvent);
    }
}
