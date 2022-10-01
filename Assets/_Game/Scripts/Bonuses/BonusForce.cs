using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusForce : Bonus
{    
    [SerializeField] private float maxForce = 5;

    private float currentForce;

    #region Injects

    private Front _front;

    [Inject]
    private void Construct(Front front)
    {
        _front = front;
    }

    #endregion

    public override void End()
    {       
        _front.ForceCrusher = currentForce;
    }

    public override void Init()
    {
        base.Init();

        if (_front.ForceCrusher != (_front.ForceCrusher + maxForce))
        {
            currentForce = _front.ForceCrusher;
        }
        
        _front.ForceCrusher += maxForce;
    }

    public override bool IsPurchasingThisBoost()
    {
        return _purchaseControl.ForceBoost;
    }
}
