using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPanelUpgaredMove : UIPanelMove
{
    #region Injects
        
    private CameraControl _cameraControl;

    [Inject]
    private void Construct(CameraControl cameraControl)
    {       
        _cameraControl = cameraControl;
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Front"))
        {
            ShowPanel();
        }
    }

    public override void CloseBtn()
    {
        _cameraControl.RobotCam();
        base.CloseBtn();
    }
}
