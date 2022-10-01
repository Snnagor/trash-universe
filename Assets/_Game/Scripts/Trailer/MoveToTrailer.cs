using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveToTrailer : MonoBehaviour
{
    [SerializeField] private ProductPlace _place;

    private Coroutine _coroutine;

    private bool takeMove;

    #region Injects

    private TrailerTrash _trailer;
    private Recycling _recycling;

    [Inject]
    private void Construct(TrailerTrash trailer,
                           Recycling recycling)
    {
        _trailer = trailer;
        _recycling = recycling;
    }

    #endregion

    private void OnTriggerStay(Collider other)
    {
        Move(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        StopMove(other.gameObject);
    }

    private void Move(GameObject other)
    {
        if (other.CompareTag("Front") && _place.AllItemConveyor.Count > 0 && !_trailer.FullTrailer && !takeMove)
        {
            takeMove = true;
            StopProcess(); 

            _coroutine = StartCoroutine(MoveToTrailerCoroutine());
        }
    }

    private IEnumerator MoveToTrailerCoroutine()
    {       
        for (int i = _place.AllItemConveyor.Count - 1; i >= 0; i--)
        {           
            if (_place.AllItemConveyor[i].CanTake && !_trailer.FullTrailer)
            {               
                _trailer.TrashContainer(_place.AllItemConveyor[i].Transform);

                _trailer.AddItemInTrailerList(_place.AllItemConveyor[i].GetIItemTrailer());

                _place.AllItemConveyor[i].KillSeq();
                _place.AllItemConveyor[i].AnimationMoveToTrailer(_place.AllItemConveyor[i].LocalPosition, 
                                                                 _trailer.GetCurrentPosition(), 
                                                                 _trailer.SizeTrashPackage);

                _place.PutAwayProduct();
                _place.AllItemConveyor[i].CanTake = false;               
                _place.RemoveItemFromList(i);
            }

            yield return new WaitForSeconds(0.01f);
        }


        takeMove = false;
        AfterMove();
    }

    public virtual void AfterMove() 
    {
        _recycling.StartRecycling();
    }

    public virtual void StopProcess()
    {
        _recycling.StopRecycling();
    }

    private void StopMove(GameObject other)
    {
        if (other.CompareTag("Front"))
        {           
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                takeMove = false;
            }

            AfterMove();
        }
    }
}
