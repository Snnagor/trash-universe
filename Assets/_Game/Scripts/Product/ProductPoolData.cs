using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ProductMove))]
public class ProductPoolData : ItemForSpawn
{
    public int IdResource { get; set; }

    protected Vector3[] pathConveyor;
    protected Transform startPathConveyor;

    #region Injects

    protected Recycling _recycling;

    [Inject]
    private void Construct(Recycling recycling)
    {
        _recycling = recycling;

        startPathConveyor = _recycling.StartPathConveyor;
    }

    #endregion

    private ProductMove _productMove;

    private void Awake()
    {
        _productMove = GetComponent<ProductMove>();
    }
    
    public void PutAwayProduct()
    {
        _recycling.Resources[IdResource].PutAwayProduct();
    }

    public void Deactivate()
    {
        transform.SetParent(RootParent);
        transform.position = Vector3.zero;
        gameObject.SetActive(false);        
    }

    public void SpawnPoolForRecycling(int idResource, Transform parent, float size)
    {
        SpawnPool(idResource, parent, size);
        transform.localPosition = startPathConveyor.localPosition;

        pathConveyor = _recycling.PathConveyor;
        _productMove.AnimationMovePathConveyor(pathConveyor);
    }

    private void SpawnPool(int idResource, Transform parent, float size)
    {
        transform.SetParent(parent);
        
        transform.localScale = new Vector3(size, size, size);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        IdResource = idResource;
        _productMove.Cost = _recycling.Resources[IdResource].GetCostProduct();
    }

    public void SpawnPoolForTrailer(int idResource, Transform parent, float size)
    {
        SpawnPool(idResource, parent, size);
    }

    public ProductMove GetProducMove()
    {
        return _productMove;
    }

}
