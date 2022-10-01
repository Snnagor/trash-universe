using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class ProductMove : MoveToConveyor, IItemTrailer
{     
    public Vector3 Position
    {
        set
        {
            transform.localPosition = value;
        }
    }
    public float Cost { get; set; }        

    #region Injects    

    private ProductPlace _productPlace;    

    [Inject]
    private void Construct(ProductPlace productPlace)
    {
        _productPlace = productPlace;       
    }

    #endregion

    public override void AnimationMoveToTrailer(Vector3 _startPosition, Vector3 _endPosition, float _sizeTrashPackage)
    {
        data.PutAwayProduct();

        base.AnimationMoveToTrailer(_startPosition, _endPosition, _sizeTrashPackage);
    }

    public override void AddProductInList()
    {
        _productPlace.AddProductList(this);
    }
    public override Vector3 GetPlace()
    {
       return _productPlace.GetPlaceForProduct(transform.localScale.x);
    }

    public void AnimationMoveItemInBuild(Vector3 _startPosition, Vector3 _endPosition, int idBuild)
    {
        Vector3[] Path = new Vector3[2];

        Vector3 middlePath = Vector3.Lerp(_startPosition, _endPosition, 0.5f);

        Path[0] = new Vector3(middlePath.x, middlePath.y + 10f, middlePath.z);
        Path[1] = _endPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalPath(Path, 1f, PathType.CatmullRom, PathMode.Full3D, 10).SetEase(Ease.InOutQuad).OnComplete(() => DeactivateFactory(idBuild)));
        seq.Join(transform.DOScale(1f, 1f));
    }

    public void DeactivateFactory(int idBuild)
    {
        _scoreManager.TmpMoney += Cost;
        _tradeTeleport.FlashPlay();
        data.Deactivate();
    }

    public override IItemTrailer GetIItemTrailer()
    {
        return this;
    }
}
