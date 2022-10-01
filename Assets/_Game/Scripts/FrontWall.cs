using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FrontWall : MonoBehaviour
{    
    [SerializeField] private GameObject warningBox;

    private bool isHere;

    #region Injects

    private SignalBus _signalBus;    

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;       
    }

    #endregion

    public void OpenFrontWall()
    {        
        warningBox.SetActive(false);
    }

    public void CloseFrontWall()
    {       
        warningBox.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Front") || other.CompareTag("Full"))
        {
            isHere = true;
            _signalBus.Fire(new FindingPlatformSignal { value = isHere });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Front") || other.CompareTag("Full"))
        {
            isHere = false;
            _signalBus.Fire(new FindingPlatformSignal { value = isHere });
        }
    }

}
