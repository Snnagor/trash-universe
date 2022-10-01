using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class UpgradeSizeStars : MonoBehaviour
{
    [SerializeField] private float deltaSize = 0.25f;
    [SerializeField] private float deltaAxe = 0.07f;
    [SerializeField] private float deltaSizeLadle = 0.17f;
    [SerializeField] private float deltaSizeLadleZ = 0.07f;

    [SerializeField] private Transform axe;
    [SerializeField] private Transform ladle;
    [SerializeField] private Transform mainParentStars;
    [SerializeField] private float deltaPosX = 0.041f;
    [SerializeField] private float deltaPosY = 0.033f;

    private UpgradeLadle upgradeLadle;


    private float currentSizeStars = 1f;
    private float currentSizeAxe = 1f;
    private float currentSizeLadle = 1f;
    private float currentSizeLadleZ = 1f;

    #region Injects

    private UpgradeSizeCollider _upgradeSizeCollider;

    [Inject]
    private void Construct(UpgradeSizeCollider upgradeSizeCollider)
    {
        _upgradeSizeCollider = upgradeSizeCollider;
    }

    #endregion

    private void Awake()
    {
        upgradeLadle = GetComponent<UpgradeLadle>();
    }

    public void ChangeSize()
    {
        SetSizeLadle();
        SetPositionLadle();
        _upgradeSizeCollider.ChangeCollider();
    }

    private void SetSizeLadle()
    {
        currentSizeStars += deltaSize;

        if (upgradeLadle != null)
            upgradeLadle.SetSizeStar(currentSizeStars);

        currentSizeLadle += deltaSizeLadle;
        currentSizeLadleZ += deltaSizeLadleZ;
        ladle.localScale = new Vector3(currentSizeLadle, currentSizeLadle, currentSizeLadleZ);

        currentSizeAxe += deltaAxe;
        axe.localScale = new Vector3(currentSizeAxe, currentSizeAxe, currentSizeAxe);
    }

    private void SetPositionLadle()
    {
        if (upgradeLadle != null)
            upgradeLadle.ChangeWidthLadle();

        ladle.localPosition = new Vector3(ladle.localPosition.x + deltaPosX,
                                                  ladle.localPosition.y + deltaPosY,
                                                  ladle.localPosition.z);

        axe.localPosition = new Vector3(axe.localPosition.x + deltaPosX,
                                        axe.localPosition.y + deltaPosY,
                                        axe.localPosition.z);

        mainParentStars.localPosition = new Vector3(mainParentStars.localPosition.x + deltaPosX,
                                                    mainParentStars.localPosition.y + deltaPosY,
                                                    mainParentStars.localPosition.z);
    }

    public void SetCurrentSize()
    {
        if (upgradeLadle != null)
            upgradeLadle.SetSizeStar(currentSizeStars);
    }

}
