using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.UI;

public abstract class PlatformBuy : MonoBehaviour
{
    [SerializeField] protected PlatformMoveHorizontal platform;
    [SerializeField] private SpriteRenderer panel;
    [SerializeField] private Text title;
    [SerializeField] private Text paid;
    [SerializeField] private Text arrow;
    [SerializeField] private Text cost;
    [SerializeField] private float durationEnable;

    #region Injects

    protected PlatformManager PlatformManager;
    protected ScoreManager ScoreManager;
    private SignalBus _signalBus;    

    [Inject]
    private void Construct(PlatformManager platformManager, 
                           ScoreManager scoreManager,
                           SignalBus signalBus)
    {
        this.PlatformManager = platformManager;
        ScoreManager = scoreManager;
        _signalBus = signalBus;
    }

    #endregion

    public virtual void Start()
    {
        if (platform.Built)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Front"))
        {           
            BuyPlatform();
        }
    }

    public abstract void BuyPlatform();
    public abstract int NeedScore();

    public virtual void ShowPlatform()
    {
        gameObject.SetActive(false);
        platform.Arrival();
    }

    public void ShowBuyButton()
    {
        gameObject.SetActive(true);
        
        panel.DOFade(0.9f, durationEnable);
        title.DOFade(1f, durationEnable);
        paid.DOFade(1f, durationEnable);
        arrow.DOFade(1f, durationEnable);
        cost.DOFade(1f, durationEnable);
    }

    public void EnableButtonBuy()
    {
        gameObject.SetActive(true);
        
        var nowColor = panel.color;
        
        panel.color = new Color(nowColor.r, nowColor.g, nowColor.b, 0.9f);
        title.color = new Color(title.color.r, title.color.g, title.color.b, 1f);
        paid.color = new Color(paid.color.r, paid.color.g, paid.color.b, 1f);
        arrow.color = new Color(arrow.color.r, arrow.color.g, arrow.color.b, 1f);
        cost.color = new Color(cost.color.r, cost.color.g, cost.color.b, 1f);
    } 

    public void DisableButtonBuy()
    {
        gameObject.SetActive(false);
        
        var nowColor = panel.color;
        
        panel.color = new Color(nowColor.r, nowColor.g, nowColor.b, 0f);
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0f);
        paid.color = new Color(paid.color.r, paid.color.g, paid.color.b, 0f);
        arrow.color = new Color(arrow.color.r, arrow.color.g, arrow.color.b, 0f);
        cost.color = new Color(cost.color.r, cost.color.g, cost.color.b, 0f);
    }
}
