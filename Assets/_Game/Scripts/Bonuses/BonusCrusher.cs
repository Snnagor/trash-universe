using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusCrusher : Bonus
{    

    #region Injects

    private UpgradeSizeCollider _upgradeSizeCollider;

    [Inject]
    private void Construct(UpgradeSizeCollider upgradeSizeCollider)
    {
       _upgradeSizeCollider = upgradeSizeCollider;
    }

    #endregion

    public override void Init()
    {
        base.Init();
        _upgradeSizeCollider.BonusCrusherOn();
    }      

    public override void End()
    {
        _upgradeSizeCollider.BonusCrusherOff();
    }

    public override bool IsPurchasingThisBoost()
    {
        return _purchaseControl.CrusherBoost;
    }
}
