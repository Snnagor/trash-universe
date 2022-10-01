using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesManager : MonoBehaviour
{
    [Header("Hats")]
    [SerializeField] private GameObject emptyHead;
    [SerializeField] private GameObject headphones;

    public void LocalAwake()
    {
        SetEmptyHead();
    }

    private GameObject currentHat;

    private void ChangeHat(GameObject hat)
    {
        if (currentHat != null)
        {
            currentHat.gameObject.SetActive(false);
        }

        currentHat = hat;

        if (currentHat != null)
            currentHat.gameObject.SetActive(true);
    }

    public void SetEmptyHead()
    {
        ChangeHat(emptyHead);
    }

    public void SetHadphones()
    {
        ChangeHat(headphones);
    }
}
