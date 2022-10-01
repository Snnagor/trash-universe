using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildAnimation : MonoBehaviour
{
    [SerializeField] private int idBuild;

    [SerializeField] private Transform _placeCollectedTrash;

    public int IdBuild { get => idBuild;}

    public Vector3 GetPlaceCollectedTrash()
    {
        return _placeCollectedTrash.localPosition;
    }    
}
