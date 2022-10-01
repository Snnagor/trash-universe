using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class UIPanelMove : MonoBehaviour
{
    [SerializeField] private RectTransform upgardePandel;
    [SerializeField] private float duration;
    [SerializeField] private float startPosY;
    [SerializeField] private float endPosY;

    #region Injects

    private FloatingJoystick _floatingJoystick;
    protected ScoreManager _scoreManager;
    protected BtnContoller _btnController;
    protected AdsManager _adsManager;

    [Inject]
    private void Construct(FloatingJoystick floatingJoystick,
                           ScoreManager scoreManager,
                           BtnContoller btnController,
                           AdsManager adsManager)
    {
        _floatingJoystick = floatingJoystick;
        _scoreManager = scoreManager;
        _btnController = btnController;
        _adsManager = adsManager;
    }

    #endregion

    private void Start()
    {
        upgardePandel.anchoredPosition = new Vector2(upgardePandel.anchoredPosition.x, startPosY);
    }

    public virtual void ShowPanel()
    {
        _floatingJoystick.Controll = false;
        upgardePandel.DOAnchorPosY(endPosY, duration).SetEase(Ease.OutBack);
    }

    public virtual void HidePanel()
    {
        _floatingJoystick.Controll = true;
        upgardePandel.DOAnchorPosY(startPosY, duration).SetEase(Ease.InBack).OnComplete(CallBackHide);
    }

    public virtual void CloseBtn()
    {               
        HidePanel();
    }

    public virtual void CallBackHide()
    {

    }
}
