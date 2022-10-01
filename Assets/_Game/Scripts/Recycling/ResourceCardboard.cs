using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceCardboard : Resource
{
    [Inject]
    private CreatePizzaBox _createPizzaBox;

    public override void UIUpdateProduct()
    {
        _viewModelRecycling.PizzaBox = Products[0].CountProduct.ToString();
    }

    public override void UIUpdateResourses(int count)
    {
        _viewModelRecycling.Cardboard = count.ToString();
    }

    public override void CreateProduct(int idResource, Transform parent, float size)
    {
        var newPizzaBox = _createPizzaBox.NewItem(idResource, parent, size);
    }

    public override ProductPoolData CreateProductForTrailer(int idResource, Transform parent, float size)
    {
        var newPizzaBox = _createPizzaBox.NewItemForTrailer(idResource, parent, size);

        return newPizzaBox;
    }

    public override void UIUpdateInfo()
    {
        _viewModelRecycling.CostCardboardToPizzaBox = Products[0].CostProductToResource.ToString();
    }
}

