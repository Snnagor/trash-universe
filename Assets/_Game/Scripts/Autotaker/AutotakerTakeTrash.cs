using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutotakerTakeTrash : MonoBehaviour
{
    [SerializeField] private AutotakerTrashControl trashControl;
    [SerializeField] private Transform handContainer;
    [SerializeField] private Transform destroyParticalPosition;
    [SerializeField] private AutotakerTrashInHand[] trashInHandPrefab;

    private enumTypeTrash _currentTypeTrash;
    private AutotakerTrashInHand _newTrashinHand;
    private bool _particalIsPlaying;

    public void TakeTrash()
    {
        trashControl.TakeTrash();
        _currentTypeTrash = trashControl.GetTypeTrash();
        CreateTrashInHand(_currentTypeTrash.GetHashCode());

        if(_particalIsPlaying)
        {
            _particalIsPlaying = false;
        }
    }
    
    private void CreateTrashInHand(int indexTrash)
    {
        _newTrashinHand = Instantiate(trashInHandPrefab[indexTrash], handContainer);
        _newTrashinHand.transform.localPosition = Vector3.zero;
    }

    public void CanTakeTrash()
    {
        trashControl.OffTakeTrash(_currentTypeTrash);
    }

    
    public void ThrowTrash()
    {
        _newTrashinHand.FallTrash();
        Destroy(_newTrashinHand.gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Trash") && !_particalIsPlaying)
        {
            _particalIsPlaying = true;
            var newDestroyParticle = Instantiate(_newTrashinHand.DestroyParticale, transform);
            newDestroyParticle.transform.localPosition = destroyParticalPosition.localPosition;
        }        
    }
}
