using UnityEngine;

public interface IItemConveyor
{
    public bool CanTake {get; set;}

    public Transform Transform { get; }
    public Vector3 LocalPosition { get;}
    public void AnimationMoveToTrailer(Vector3 _startPosition, Vector3 _endPosition, float _sizeTrashPackage);

    public void KillSeq();
}
