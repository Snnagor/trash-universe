using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum TypeBuild
{
    Recycling,
    Autotaker,
    Upgrade
}

public class ShowPlatform : MonoBehaviour
{
    [SerializeField] private TypeBuild typeBuild;

    #region Injects

    protected CameraControl _cameraControl;

    [Inject]
    private void Construct(CameraControl cameraControl)
    {
        _cameraControl = cameraControl;
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {        
        ChangeCam(other);
    }

    public virtual void ChangeCam(Collider other)
    {
        if (other.CompareTag("Front"))
        {
            switch (typeBuild.GetHashCode())
            {
                case 0: _cameraControl.RecyclingCam(); break;
                case 1: _cameraControl.AutotakerCam(); break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Front"))
        {            
            _cameraControl.RobotCam();
        }            
    }

    public int GetIdTypeBuild()
    {
        return typeBuild.GetHashCode();
    }
}
