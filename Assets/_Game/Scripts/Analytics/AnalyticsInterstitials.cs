using com.adjust.sdk;
using UnityEngine;
using Zenject;

public class AnalyticsInterstitials : MonoBehaviour
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
        AdjustEvent adjustEvent = new AdjustEvent("m32scg");
        adjustEvent.setCallbackId("it_finish");        
        adjustEvent.addCallbackParameter("time", _analyticsTimerService.CurrentMinutTime.ToString());

        Adjust.trackEvent(adjustEvent);
    }
}
