using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AutotakerResourceMove : MoveToConveyor
{
    [SerializeField] private TrashPackedData resourceData;
    [SerializeField] private TrashPacked trashPacked;

    #region Injects    

    private ResourcePlace _resourcePlace;   

    [Inject]
    private void Construct(ResourcePlace presourcePlace)
    {        
        _resourcePlace = presourcePlace;
    }

    #endregion

    public override void AddProductInList()
    {
        _resourcePlace.AddProductList(this);
    }

    public override IItemTrailer GetIItemTrailer()
    {
        return trashPacked;
    }

    public override Vector3 GetPlace()
    {
        return _resourcePlace.GetPlaceForProduct(transform.localScale.x);
    }

    public int GetTypeResource()
    {
        return resourceData.IndexType;
    }
}
