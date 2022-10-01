using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCleaning : MonoBehaviour
{
    [SerializeField] private BoxCollider _normalBoxCollider;
    [SerializeField] private BoxCollider _magnetBonusBoxCollider;    
    [SerializeField] private BoxCollider _crusherBonusBoxCollider;
    [SerializeField] private TrailerTrash _trailer;

    #region Injects

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;        
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("TrashPacked"))
        {           
            Cleaning(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("TrashPacked"))
        {           
            Cleaning(collision.gameObject);
        }
    }

    private void Cleaning(GameObject other)
    {       
        if (!_trailer.FullTrailer)
        {            
            other.GetComponent<TrashPacked>().MoveTrashPackedInTrailer(_trailer);         
        }
    }

    public void EnableMagnetBonusCollider(bool value)
    {
        _magnetBonusBoxCollider.enabled = value;
    }

    public void EnableCrusherBonusCollider(bool value)
    {
        _crusherBonusBoxCollider.enabled = value;
    }

    public BoxCollider GetTakeCollider()
    {
        return _normalBoxCollider;
    }
}
