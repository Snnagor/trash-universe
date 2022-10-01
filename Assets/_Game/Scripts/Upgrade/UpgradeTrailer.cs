using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTrailer : MonoBehaviour
{
    [SerializeField] private TrailerTrash trailer;
    [SerializeField] private int startPlace = 200;
    [SerializeField] private int[] trailerSpace;

    private int indexCurrentUpgrade;

    private void Start()
    {
        SetSpaceTrailer(startPlace);
    }

    public void ChangeSpaceTrailer()
    {
        if (indexCurrentUpgrade == trailerSpace.Length) return;

        SetSpaceTrailer(trailerSpace[indexCurrentUpgrade]);
        indexCurrentUpgrade++;
    }

    private void SetSpaceTrailer(int trailerSpace)
    {
        trailer.MaxHeight = trailerSpace / (trailer.MaxCols * trailer.MaxRows);

        trailer.UpdateIndicatorTrailer();
        trailer.SetFullTrailerFalse(); 
    }
}
