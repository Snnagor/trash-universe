using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceTire : Resource
{
    [Inject]
    private CreateDuck _createDuck;

    public override void UIUpdateProduct()
    {
        _viewModelRecycling.LittleDuck = Products[0].CountProduct.ToString();
    }

    public override void UIUpdateResourses(int count)
    {
        _viewModelRecycling.Tire = count.ToString();
    }

    public override void CreateProduct(int idResource, Transform parent, float size)
    {
       var newDuck = _createDuck.NewItem(idResource, parent, size);
    }

    public override ProductPoolData CreateProductForTrailer(int idResource, Transform parent, float size)
    {
        var newDuck = _createDuck.NewItemForTrailer(idResource, parent, size);

        return newDuck;
    }

    public override void UIUpdateInfo()
    {       
        _viewModelRecycling.CostTireToDuck = Products[0].CostProductToResource.ToString();
    }
}
