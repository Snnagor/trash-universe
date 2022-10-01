using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PlayerMoveInBus : MonoBehaviour
{
    [SerializeField] private float durationMove = 2;
    [SerializeField] private float durationRotation = 1;

    #region Injects

    private BusMove _busMove;    

    [Inject]
    private void Construct(BusMove busMove)
    {
        _busMove = busMove;
    }

    #endregion

    public void RobotMove(Vector3 targetMove)
    {
        Sequence seq = DOTween.Sequence();
               
        seq.Append(transform.DOMoveX(targetMove.x, durationMove).SetEase(Ease.OutQuint));
        seq.Join(transform.DOMoveZ(targetMove.z, durationMove).SetEase(Ease.OutQuint).OnComplete(CallBackArrival));
        seq.Join(transform.DOLookAt(targetMove, durationRotation));
    }

    private void CallBackArrival()
    {
        gameObject.SetActive(false);
        _busMove.DepartureBus();
    }
}
