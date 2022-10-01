using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class PlatformNewButton : MonoBehaviour
{
    [SerializeField] private Transform button;
    [SerializeField] private float onPositionY = 0.1f;
    [SerializeField] private float offPositionY = -0.05f;
    [SerializeField] private float durationAnimOn = 0.5f;
    [SerializeField] private float durationAnimOff = 0.3f;
    [SerializeField] private MeshRenderer meshButton;
    [SerializeField] private Material materialOnButton;
    [SerializeField] private Material materialOffButton;

    [Header("rightBillboard")]
    [SerializeField] private SpriteRenderer rightBillboard;
    [SerializeField] private Sprite lockNewPlatformButton;
    [SerializeField] private Sprite unlockNewPlatformButton;

    private bool buttonPressed;

    #region Injects

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    #endregion

    private void Start()
    {
        StartButtonOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Front") && !buttonPressed)
        {
            _signalBus.Fire(new NewPlatformSignal());

            SetButtonOff();
        }
    }

    public void SetButtonOn()
    {        
        AnimOn();
    }

    public void SetButtonOff()
    {       
        AnimOff();
    }

    public void StartButtonOn()
    {
        ButtonOnMat();        
        button.localPosition = new Vector3(button.localPosition.x, onPositionY, button.localPosition.z);
    }    

    public void StartButtonOff()
    {
        ButtonOffMat();        
        button.localPosition = new Vector3(button.localPosition.x, offPositionY, button.localPosition.z);
    }

    public void AnimOn()
    {
        if (button.transform.localPosition.y < onPositionY)
            button.DOLocalMoveY(onPositionY, durationAnimOn).SetEase(Ease.InBack).OnComplete(ButtonOnMat);
    }

    public void AnimOff()
    {
        if (button.transform.localPosition.y > offPositionY)
            button.DOLocalMoveY(offPositionY, durationAnimOff).OnComplete(ButtonOffMat);
    }

    private void ButtonOnMat()
    {
        buttonPressed = false;
        meshButton.material = materialOnButton;
        rightBillboard.sprite = unlockNewPlatformButton;
    }

    private void ButtonOffMat()
    {
        buttonPressed = true;
        meshButton.material = materialOffButton;
        rightBillboard.sprite = lockNewPlatformButton;        
    }


}
