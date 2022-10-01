using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnalyticsFirstPurchase : MonoBehaviour
{
    #region Injects

    private AnalyticsTimerService _analyticsTimerService;

    [Inject]
    private void Construct(AnalyticsTimerService analyticsTimerService)
    {
        _analyticsTimerService = analyticsTimerService;
    }

    #endregion

    public void SetEvent()
    {       
        AdjustEvent adjustEvent = new AdjustEvent("zd3mtv");
        adjustEvent.setCallbackId("unique_pu");
        adjustEvent.addCallbackParameter("time", _analyticsTimerService.CurrentMinutTime.ToString());

        Adjust.trackEvent(adjustEvent);
    }
}
