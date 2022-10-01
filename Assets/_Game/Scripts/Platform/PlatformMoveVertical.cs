using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public abstract class PlatformMoveVertical : MonoBehaviour
{
    [SerializeField] protected float startPositionX;
    [SerializeField] protected float startPositionY;
    [SerializeField] protected float startPositionZ;

    [SerializeField] protected float finishPositionX; 
    [SerializeField] protected float finishPositionY;
    [SerializeField] protected float finishPositionZ;
    [SerializeField] protected float duration;

    #region Injects

    protected SignalBus _signalBus;
    protected PlatformManager _platformManager;
    protected PlatformNewButton _newPlaformButton;

    [Inject]
    private void Construct(SignalBus signalBus,
                           PlatformManager platformManager,
                           PlatformNewButton newPlaformButton)
    {
        _signalBus = signalBus;
        _platformManager = platformManager;
        _newPlaformButton = newPlaformButton;
    }

    #endregion

   
    public virtual void Start()
    {
        StartPosition();
    }

    public virtual void StartPosition()
    {
        transform.localPosition = new Vector3(startPositionX, startPositionY, startPositionZ);
    }

    public virtual void FinishPosition()
    {
        transform.localPosition = new Vector3(finishPositionX, finishPositionY, finishPositionZ);
    }

    public virtual void Arrival()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveY(finishPositionY, duration).SetEase(Ease.OutBack).OnStart(CallBackStartArrival));
        seq.Append(transform.DOLocalMoveZ(finishPositionZ, duration).OnComplete(CallBackArrival));
        seq.Join(transform.DOLocalMoveX(finishPositionX, duration));
    }

    public virtual void Departure()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveZ(startPositionZ, duration).OnStart(CallBackDeparture));
        seq.Join(transform.DOLocalMoveX(startPositionX, duration));
        seq.Append(transform.DOLocalMoveY(startPositionY, duration).SetEase(Ease.OutExpo));
    }

    public virtual void ChangePlatform()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveZ(startPositionZ, duration).OnStart(CallBackDeparture));
        seq.Join(transform.DOLocalMoveX(startPositionX, duration));
        seq.Append(transform.DOLocalMoveY(startPositionY, duration).SetEase(Ease.OutExpo).OnComplete(CallBackChangePlatform));
        seq.Append(transform.DOLocalMoveY(finishPositionY, duration).SetEase(Ease.OutBack));
        seq.Append(transform.DOLocalMoveZ(finishPositionZ, duration).OnComplete(CallBackArrival));
        seq.Join(transform.DOLocalMoveX(finishPositionX, duration));
    }


    public abstract void CallBackArrival();
    public abstract void CallBackDeparture(); 
    public abstract void CallBackChangePlatform();
    public abstract void CallBackStartArrival();

}
