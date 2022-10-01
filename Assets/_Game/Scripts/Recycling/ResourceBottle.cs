using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceBottle : Resource
{
    [Inject]
    private CreateGlasses _createGlasses;

    public override void UIUpdateProduct()
    {
        _viewModelRecycling.Glasses = Products[0].CountProduct.ToString();
    }

    public override void UIUpdateResourses(int count)
    {
        _viewModelRecycling.Bottle = count.ToString();
    }

    public override void CreateProduct(int idResource, Transform parent, float size)
    {
        var newGlasses = _createGlasses.NewItem(idResource, parent, size);
    }

    public override ProductPoolData CreateProductForTrailer(int idResource, Transform parent, float size)
    {
        var newGlasses = _createGlasses.NewItemForTrailer(idResource, parent, size);

        return newGlasses;
    }

    public override void UIUpdateInfo()
    {
        _viewModelRecycling.CostBottleToGlasses = Products[0].CostProductToResource.ToString();
    }
}
