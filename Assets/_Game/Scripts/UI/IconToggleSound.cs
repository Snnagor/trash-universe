using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IconToggleSound : IconToggle
{    

    #region Signals

    private void OnEnable()
    {
        signalBus.Subscribe<SoundSignal>(CallbackSignal);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<SoundSignal>(CallbackSignal);
    }

    #endregion

    protected override void Start()
    {
        iconBool = soundManager.EnableSound;

        base.Start();
    }
}
