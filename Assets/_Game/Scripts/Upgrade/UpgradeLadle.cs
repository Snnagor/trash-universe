using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLadle : MonoBehaviour
{
    [SerializeField] private UpgradeStars[] starsGroup;
    [SerializeField] private int startEnableStar = 2;
    [SerializeField] private float widthLadle = 1.4f;
    [SerializeField] private float deltaWidthLadle = 0.12f;

    public UpgradeStars CurrentStarsGroup { get; set; }

    private UpgradeSizeStars upgradeSize;

    private float distanceBetweenStar;
    private int currentEnableStar;
    private int indexCurrentGroupStar;

    private void Awake()
    {
        upgradeSize = GetComponent<UpgradeSizeStars>();

        InitUpgradeLadle();
    }

    private void Start()
    {

    }

    public void InitUpgradeLadle()
    {
        ChangeGroup(starsGroup[indexCurrentGroupStar]);

        currentEnableStar = startEnableStar;
        SetPositionStar();
    }

    private void ChangeGroup(UpgradeStars group)
    {
        if (CurrentStarsGroup != null)
        {
            CurrentStarsGroup.gameObject.SetActive(false);
        }

        CurrentStarsGroup = group;

        if (CurrentStarsGroup != null)
            CurrentStarsGroup.gameObject.SetActive(true);
    }

    public void AddStar()
    {
        if (currentEnableStar == CurrentStarsGroup.Stars.Length) return;

        EnableOneStar();
        SetPositionStar();

    }

    public void EnableOneStar()
    {
        if (CurrentStarsGroup != null)
        {
            CurrentStarsGroup.Stars[currentEnableStar].gameObject.SetActive(true);
            currentEnableStar++;
        }
    }

    public void ChangeWidthLadle()
    {
        widthLadle += deltaWidthLadle;
        SetPositionStar();
    }

    public void ChangeGroupStar()
    {
        if (indexCurrentGroupStar == starsGroup.Length - 1) return;

        indexCurrentGroupStar++;
        ChangeGroup(starsGroup[indexCurrentGroupStar]);
        EnableStarInNewGroup();
        SetPositionStar();
        upgradeSize.SetCurrentSize();
    }

    public void SetPositionStar()
    {
        distanceBetweenStar = widthLadle / currentEnableStar;

        float positionZ = 0;
        int startPos = 0;
        bool isEven;

        if (currentEnableStar % 2 == 0)
        {
            isEven = true;
            startPos = 1;
        }
        else
        {
            isEven = false;
        }

        for (int i = 0; i < currentEnableStar; i++)
        {
            if (isEven)
            {
                positionZ = EvenCountStarPosition(ref startPos, i);
            }
            else
            {
                positionZ = OddCountStarPosition(ref startPos, i);
            }

            CurrentStarsGroup.Stars[i].transform.localPosition = new Vector3(CurrentStarsGroup.Stars[i].transform.localPosition.x,
                                                                             CurrentStarsGroup.Stars[i].transform.localPosition.y,
                                                                             positionZ);
        }
    }

    private float OddCountStarPosition(ref int startPos, int i)
    {
        float positionZ;
        if (i != 0 && i % 2 == 1)
        {
            if (startPos < 0) startPos *= -1;
            startPos++;
        }
        else if (i != 0 && i % 2 == 0)
        {
            startPos *= -1;
        }

        positionZ = startPos * distanceBetweenStar;

        return positionZ;
    }

    private float EvenCountStarPosition(ref int startPos, int i)
    {
        float positionZ;
        if (i != 0 && i % 2 == 0)
        {
            if (startPos < 0) startPos *= -1;
            startPos++;
        }
        else if (i != 0 && i % 2 == 1)
        {
            startPos *= -1;
        }

        if (i > 1)
        {
            if (startPos > 0) positionZ = (startPos * distanceBetweenStar) - (distanceBetweenStar / 2);
            else positionZ = (startPos * distanceBetweenStar) + (distanceBetweenStar / 2);
        }
        else
        {
            positionZ = startPos * distanceBetweenStar / 2;
        }

        return positionZ;
    }

    private void EnableStarInNewGroup()
    {
        for (int i = 0; i < currentEnableStar; i++)
        {
            CurrentStarsGroup.Stars[i].gameObject.SetActive(true);
        }
    }

    public void SetSizeStar(float size)
    {
        foreach (var item in CurrentStarsGroup.Stars)
        {
            item.transform.localScale = new Vector3(size, size, size);
        }
    }
}
