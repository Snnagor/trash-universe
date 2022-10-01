using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconToggleVibro : IconToggle
{
    #region Signals

    private void OnEnable()
    {
        signalBus.Subscribe<VibroSignal>(CallbackSignal);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<VibroSignal>(CallbackSignal);
    }

    #endregion

    protected override void Start()
    {
        iconBool = soundManager.EnableVibro;

        base.Start();
    }
}
