using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IconToggle : MonoBehaviour
{
    [SerializeField] protected Sprite offIcon;
    [SerializeField] protected Sprite onIcon;

    protected Image btn;

    protected bool iconBool = true;

    #region Injects

    protected SignalBus signalBus;
    protected SoundManager soundManager;

    [Inject]
    private void Construct(SignalBus _signalBus, 
                           SoundManager _soundManager)
    {
        signalBus = _signalBus;
        soundManager = _soundManager;
    }

    #endregion   


    protected void CallbackSignal()
    {
        IconBtn();
    }       

    private void Awake()
    {
        btn = GetComponent<Image>();
    }

    protected virtual void Start()
    {       
        if (iconBool)
            btn.sprite = onIcon;
        else
            btn.sprite = offIcon;
    }

    public void IconBtn()
    {
        if (iconBool)
        {
            iconBool = false;
            btn.sprite = offIcon;
        }
        else
        {
            iconBool = true;
            btn.sprite = onIcon;
        }
    }



}
