using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformTrashMove : PlatformMoveVertical
{
    [SerializeField] private float timeArrival;

    private PlatformMainTrashControl trashControl;

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<NewPlatformSignal>(NewPlatformSignal);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<NewPlatformSignal>(NewPlatformSignal);
    }

    private void NewPlatformSignal()
    {
        if (transform.position.z == finishPositionZ)
        {
            ChangePlatform();
        }
    }

    #endregion

    private void Awake()
    {
        trashControl = GetComponent<PlatformMainTrashControl>();
    }

    public override void Start()
    {        
        if(timeArrival > 0)
        {
            Invoke("Arrival", timeArrival);
        }
        
        base.Start();
    }

    public virtual void NewTrash()
    {
        trashControl.NewTrashSignal();
    }

    public override void CallBackArrival()
    {      
        _signalBus.Fire(new MainTrashPlatformArrivalSignal());
    }

    public override void CallBackDeparture()
    {
        _platformManager.CloseFrontWall();
        _newPlaformButton.SetButtonOff();
    }

    public override void CallBackChangePlatform()
    {
        NewTrash();
    }

    public override void CallBackStartArrival()
    {        
        trashControl.StartTrashNullNewTrash();
    }
}
