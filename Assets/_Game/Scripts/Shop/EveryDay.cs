using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Zenject;

public class EveryDay : MonoBehaviour
{
    [SerializeField] private Button everyDay;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite imageButtonOff;
    [SerializeField] private Sprite imageButtonOn;

    private float currentClaimTime = -2;

    #region Injects

    private SettingsShop _settingsShop;
    private SerialDataManager _serialDataManager;

    [Inject]
    private void Construct(SettingsShop settingsShop,
                           SerialDataManager serialDataManager)
    {
        _settingsShop = settingsShop;
        _serialDataManager = serialDataManager;
    }

    #endregion

    public void Off()
    {       
        everyDay.interactable = false;
        imageButton.sprite = imageButtonOff;
    }

    public void On()
    {
        everyDay.interactable = true;
        imageButton.sprite = imageButtonOn;
    }

    private ulong lastOpen;

    void Start()
    {      
        lastOpen = _serialDataManager.Data.LastClaimTime;

        if (!isReady())
        {
            Off();
        }
    }
    
    void Update()
    {
        if (!everyDay.IsInteractable())
        {
            if (isReady())
            {
                On();

                return;
            }
        }
    }

    public void Click()
    {
        lastOpen = (ulong)DateTime.Now.Ticks;
        _serialDataManager.Data.LastClaimTime = lastOpen;

        Off();
    }

    private bool isReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconleft = (float)(_settingsShop.EveryDayDeltaTime - m) / 1000.0f;

        if (seconleft < 0)
        {
            return true;
        }

        return false;
    }
}
