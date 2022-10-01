using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnalyticsTutorial : MonoBehaviour
{
    #region Injects

    private AnalyticsTimerService _analyticsTimerService;

    [Inject]
    private void Construct(AnalyticsTimerService analyticsTimerService)
    {
        _analyticsTimerService = analyticsTimerService;
    }

    #endregion

    public void SetEvent(int step, string step_name)
    {
        AdjustEvent adjustEvent = new AdjustEvent("8oym69");
        adjustEvent.setCallbackId("tutorial");
        adjustEvent.addCallbackParameter("step", step.ToString());
        adjustEvent.addCallbackParameter("time", step_name);
        adjustEvent.addCallbackParameter("time", "Completed");
        adjustEvent.addCallbackParameter("time", _analyticsTimerService.CurrentMinutTime.ToString());

        Adjust.trackEvent(adjustEvent);
    }
}
