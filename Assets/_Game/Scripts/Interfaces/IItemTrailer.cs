using UnityEngine;

public interface IItemTrailer
{
    public int IndexType { get; }
    public float Cost { get; }
    public Transform Transform { get;}
    public Vector3 Position { set; }
    public void AnimationMoveItemInBuild(Vector3 _startPosition, Vector3 _endPosition, int idBuild);
    public void DeactivateFactory(int idBuild);
}
