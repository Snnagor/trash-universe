using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMoveHorizontal : PlatformMoveVertical
{
    [SerializeField] private GameObject ramps;
    [SerializeField] private float timeArrival;
    [SerializeField] protected GameObject warningWall;

    public bool Built { get; set; }

    public override void Start()
    {
        if (Built)
        {
            FinishPosition();
        }
        else
        {
            StartPosition();
        }
    }

    public override void StartPosition()
    {        
        if (ramps.activeSelf) ramps.SetActive(false);
        base.StartPosition();
    }

    public override void FinishPosition()
    {
        CallBackArrival();
        base.FinishPosition();
    }

    public override void Arrival()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveZ(finishPositionZ, duration).OnStart(CallBackStartArrival).OnComplete(CallBackArrival));
        seq.Join(transform.DOLocalMoveX(finishPositionX, duration));
    }

    public override void Departure()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveZ(startPositionZ, duration).OnStart(CallBackDeparture));
        seq.Join(transform.DOLocalMoveX(startPositionX, duration));
        seq.Append(transform.DOLocalMoveY(startPositionY, duration).SetEase(Ease.OutQuint));
    }

    public override void ChangePlatform()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveZ(startPositionZ, duration).OnStart(CallBackDeparture));
        seq.Join(transform.DOLocalMoveX(startPositionX, duration));
        seq.Append(transform.DOLocalMoveY(startPositionY, duration).SetEase(Ease.OutQuint).OnComplete(CallBackChangePlatform));
        seq.Append(transform.DOLocalMoveY(finishPositionY, duration).SetEase(Ease.OutQuint));
        seq.Append(transform.DOLocalMoveZ(finishPositionZ, duration).OnComplete(CallBackArrival));
        seq.Join(transform.DOLocalMoveX(finishPositionX, duration));
    }

    public override void CallBackArrival()
    {
        ramps.SetActive(true);
        warningWall.SetActive(false);
        Built = true;
    }

    public override void CallBackChangePlatform()
    {
        
    }

    public override void CallBackDeparture()
    {
        ramps.SetActive(false);
    }

    public override void CallBackStartArrival()
    {
        
    }
}
