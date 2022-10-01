using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlatformUpgrade : ShowPlatform
{
    public override void ChangeCam(Collider other)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Front"))
        {            
            _cameraControl.UpgradeCam();
        }            
    }
}
