using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AutotakerMoveToTrailer : MoveToTrailer
{
    #region Injects

    private AutotakerControl _autotakerControl;

    [Inject]
    private void Construct(AutotakerControl autotaker)
    {
        _autotakerControl = autotaker;
    }

    #endregion

    public override void AfterMove()
    {       
        _autotakerControl.StartMiningAfterPauseFullPlace();
    }

    public override void StopProcess()
    {
        _autotakerControl.StopAutotaker();
    }

}
