using com.adjust.sdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnalyticsTimerService : MonoBehaviour
{
    [SerializeField] private int secondsPerMinute = 60;
    [SerializeField] private int maxMinuntTime = 60;

    public int CurrentMinutTime { get; set; }

    private float currentSecondTime = -2;

    #region Injects

    private SerialDataManager _serialDataManager;

    [Inject]
    private void Construct(SerialDataManager serialDataManager)
    {
        _serialDataManager = serialDataManager;
    }

    #endregion

    private void Start()
    {
        StartTimer();
    }

    void Update()
    {
       if(currentSecondTime > 0)
        {
            currentSecondTime -= Time.deltaTime;
        }
        else if(currentSecondTime > -1)
        {
            CurrentMinutTime++;
            SetEvent();

            if (CurrentMinutTime < maxMinuntTime)
            {
                currentSecondTime = secondsPerMinute;
            }
            else
            {
                currentSecondTime = -2;
            }
        }
    }

    private void SetEvent()
    {
        AdjustEvent adjustEvent = new AdjustEvent("n77846");
        adjustEvent.setCallbackId("timer");
        adjustEvent.addCallbackParameter("time", CurrentMinutTime.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            StartTimer();
        }        
    }

    private void StartTimer()
    {
        if (CurrentMinutTime < maxMinuntTime)
        {
            if (_serialDataManager.Data.CurrentSecondTime > 0)
            {
                currentSecondTime = _serialDataManager.Data.CurrentSecondTime;
            }
            else
            {
                currentSecondTime = secondsPerMinute;
            }

            CurrentMinutTime = _serialDataManager.Data.CurrentMinuteTime;
        }        
    }

    public void EndTimer()
    {
        _serialDataManager.Data.CurrentSecondTime = currentSecondTime;
        _serialDataManager.Data.CurrentMinuteTime = CurrentMinutTime;
        currentSecondTime = -2;
    }
}
