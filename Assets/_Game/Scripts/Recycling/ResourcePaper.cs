using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourcePaper : Resource
{   
    [Inject]
    private CreateToiletPaper _createToletPaper;

    public override void UIUpdateProduct()
    {
        _viewModelRecycling.ToiletPaper = Products[0].CountProduct.ToString();
    }

    public override void UIUpdateResourses(int count)
    {
        _viewModelRecycling.Paper = count.ToString();
    }

    public override void CreateProduct(int idResource, Transform parent, float size)
    {
       var newToiletPaper = _createToletPaper.NewItem(idResource, parent, size);
    }

    public override ProductPoolData CreateProductForTrailer(int idResource, Transform parent, float size)
    {
        var newToiletPaper = _createToletPaper.NewItemForTrailer(idResource, parent, size);

        return newToiletPaper;
    }

    public override void UIUpdateInfo()
    {       
        _viewModelRecycling.CostPaperToToiletPaper = Products[0].CostProductToResource.ToString();
    }

    
}

