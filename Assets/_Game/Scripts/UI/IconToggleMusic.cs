using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IconToggleMusic : IconToggle
{   
    #region Signals

    private void OnEnable()
    {
        signalBus.Subscribe<MusicSignal>(CallbackSignal);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<MusicSignal>(CallbackSignal);
    }
       
    #endregion


    protected override void Start()
    {
        iconBool = soundManager.EnableMusic;

        base.Start();
    }
   
}
