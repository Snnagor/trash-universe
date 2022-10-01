using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.UI;

public class BusMove : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 finishPosition;
    [SerializeField] private Vector3 continuePosition;
    [SerializeField] private float durationArrival = 3f;
    [SerializeField] private float durationDeparture = 2f;

    [SerializeField] private Transform targetForRobot;

    [SerializeField] private GameObject uiPabelComingSoon;
    [SerializeField] private Text textComingSoon;

    public bool BusArrival { get; set; } = false;

    #region Injects

    private UIController _uIController;
    private FloatingJoystick _floatingJoystick;
    private CameraControl _cameraControl;

    [Inject]
    private void Construct(UIController uIController,
                           FloatingJoystick floatingJoystick,
                           CameraControl cameraControl)
    {
        _uIController = uIController;
        _floatingJoystick = floatingJoystick;
        _cameraControl = cameraControl;
    }

    #endregion

    private void Start()
    {
        if (BusArrival)
        {
            transform.position = finishPosition;
            uiPabelComingSoon.SetActive(true);
            ComingSoonText();
        }
        else
        {
            uiPabelComingSoon.SetActive(false);
            transform.position = startPosition;
        }        
    }

    public void ArrivalBus()
    {
        Sequence seq = DOTween.Sequence();
               
        seq.Append(transform.DOMoveX(finishPosition.x, durationArrival).SetEase(Ease.InOutExpo).OnStart(CallBackOnstartArrival).OnComplete(CallBackArrival));
        seq.AppendInterval(3f).OnComplete(CallBackPause);
    }

    public void DepartureBus()
    {
        transform.DOMoveX(continuePosition.x, durationDeparture).SetEase(Ease.InExpo).OnComplete(CallBackDeparture);
    }

    private void CallBackOnstartArrival()
    {
        _floatingJoystick.Controll = false;
        _cameraControl.TutorialNextLevelCam();
    }

    private void CallBackArrival()
    {
        BusArrival = true;
        uiPabelComingSoon.SetActive(true);
        ComingSoonText();
    }

    private void CallBackPause()
    {
        _floatingJoystick.Controll = true;
        _cameraControl.RobotCam();
    }
        

    private void CallBackDeparture()
    {
        _uIController.ComingSoonPanel();
    }

    private void ComingSoonText()
    {
        textComingSoon.DOFade(1f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
    
}
