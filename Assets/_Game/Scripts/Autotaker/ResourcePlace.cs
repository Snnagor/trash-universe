using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourcePlace : ProductPlace
{
    public List<int> AllTypeResourcesInPlace { get; set; } = new List<int>();

    #region Injects
    
    private TutorialManager _tutorialManager;

    [Inject]
    private void Construct(TutorialManager tutorialManager)
    {        
        _tutorialManager = tutorialManager;
    }

    #endregion

    public override void AddProductList(MoveToConveyor product)
    {
        base.AddProductList(product);
                
        AllTypeResourcesInPlace.Add((product as AutotakerResourceMove).GetTypeResource());
    }

    public override void RemoveItemFromList(int i)
    {
        base.RemoveItemFromList(i);
        AllTypeResourcesInPlace.RemoveAt(i);
    }

    public bool IsTrailerFull()
    {
        return fullTrailer.activeSelf;
    }

    public override Vector3 GetPlaceForProduct(float size)
    {
        _tutorialManager.StopTutorialAutotaker();

        return base.GetPlaceForProduct(size);
    }
}
