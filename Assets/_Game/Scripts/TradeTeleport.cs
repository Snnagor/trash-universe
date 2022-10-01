using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TradeTeleport : BuildAnimation
{
    [SerializeField] private ParticleSystem flashPartical;

    #region Injects

    private SoundManager _soundManager;

    [Inject]
    private void Construct(SoundManager soundManager)
    {        
        _soundManager = soundManager;
    }

    #endregion

    public void FlashPlay()
    {
        flashPartical.Play();
        _soundManager.Money();
    }
}
