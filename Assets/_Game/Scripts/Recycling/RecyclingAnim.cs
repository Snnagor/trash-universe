using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecyclingAnim : BuildAnimation
{    
    [SerializeField] private Animator animDoor;

    #region Injects
    private SignalBus _signalBus;
    private SoundManager _soundManager;

    [Inject]
    private void Construct(SignalBus signalBus,
                           SoundManager soundManager)
    {
        _signalBus = signalBus;
        _soundManager = soundManager;
    }

    #endregion

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<OpenDoorRecyclingSignal>(OpenDoorRecyclingSignal);
        _signalBus.Subscribe<CloseDoorRecyclingSignal>(CloseDoorRecyclingSignal);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<OpenDoorRecyclingSignal>(OpenDoorRecyclingSignal);
        _signalBus.Unsubscribe<CloseDoorRecyclingSignal>(CloseDoorRecyclingSignal);
    }

    private void OpenDoorRecyclingSignal()
    {
        animDoor.SetBool("Open", true);
        _soundManager.RecyclingOpenDoor();
    }

    private void CloseDoorRecyclingSignal()
    {
        animDoor.SetBool("Open", false);
    }

    #endregion
}
