using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusTrailer : Bonus
{   
    [SerializeField] private int maxHeightBonus = 10000;

    private int currentMaxHeight;

    #region Injects
   
    protected TrailerTrash _trailerTrash;

    [Inject]
    private void Construct(TrailerTrash trailerTrash)
    {        
        _trailerTrash = trailerTrash;
    }

    #endregion

    public override void End()
    {        
        _trailerTrash.MaxHeight = currentMaxHeight;        
        UpdateUI();
        _trailerTrash.SetFullTrailerTrue();
    }

    public override void Init()
    {
        base.Init();

        if (_trailerTrash.MaxHeight != maxHeightBonus)
        {
            currentMaxHeight = _trailerTrash.MaxHeight;
        }
        
        _trailerTrash.MaxHeight = maxHeightBonus;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _trailerTrash.UpdateIndicatorTrailer();
        _trailerTrash.SetFullTrailerFalse();
    }

    public override bool IsPurchasingThisBoost()
    {
        return _purchaseControl.TrailerBoost;
    }

}
