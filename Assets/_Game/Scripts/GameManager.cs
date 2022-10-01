using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class PlatformData
{
    [SerializeField] private enumTypeTrash typeTrash;
    public enumTypeTrash TypeTrash { get => typeTrash; }

    [SerializeField] private int strengthTrash;
    public int StrengthTrash { get => strengthTrash; }

    [SerializeField] private int countResources;
    public int CountResources { get => countResources; }

    [SerializeField] private int countResourcesAuto;
    public int CountResourcesAuto { get => countResourcesAuto; }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private float deltaIncreaseResistTrash = 1f;
    [SerializeField] private PlatformData[] platformsInLevel;
    public PlatformData[] PlatformsInLevel { get => platformsInLevel; }

    public int IdCurrentPlatform { get; set; }
    public float CurrentIncreaseResisTrash { get; set; }
        
    public int AnalyticsCountPlatform { get; set; }

    #region Injects

    private AnalyticsProgress _analyticsProgress;

    [Inject]
    private void Construct(AnalyticsProgress analyticsProgress)
    {
        _analyticsProgress = analyticsProgress;
    }

    #endregion

    private void Start()
    {
        if (AnalyticsCountPlatform == 0)
        {
            AnalyticsCountPlatform = 1;
            _analyticsProgress.SetEvent(AnalyticsCountPlatform);
        }
    }

    public PlatformData CurrentPlatform()
    {
        return platformsInLevel[IdCurrentPlatform];
    }

    public PlatformData CurrentLoadPlatform(int idCurrentLocalPlatform)
    {
        return platformsInLevel[idCurrentLocalPlatform];
    }

    public int GetIdCurrentTrash()
    {
        return CurrentPlatform().TypeTrash.GetHashCode();
    }

    public int GetIdCurrentLoadTrash(int idCurrentLocalPlatform)
    {
        return CurrentLoadPlatform(idCurrentLocalPlatform).TypeTrash.GetHashCode();
    }

    public void NextPlatform()
    {
        if (IdCurrentPlatform < platformsInLevel.Length - 1)
        {
            IdCurrentPlatform++;
          
            if(IdCurrentPlatform == 4)
            {
                SetCurrentResistTrash();
            }
        }
        else
        {
            IdCurrentPlatform = 0;
            SetCurrentResistTrash();
        }

        AnalyticsCountPlatform++;
        _analyticsProgress.SetEvent(AnalyticsCountPlatform);
    }

    public int GetCurrentCountResources()
    {
        return CurrentPlatform().CountResources;
    }

    public int GetCurrentCountResourcesAuto()
    {
        return CurrentPlatform().CountResourcesAuto;
    }

    public int GetCurrentLoadCountResourcesAuto(int idCurrentLocalPlatform)
    {
        return CurrentLoadPlatform(idCurrentLocalPlatform).CountResourcesAuto;
    }

    private void SetCurrentResistTrash()
    {
        CurrentIncreaseResisTrash += deltaIncreaseResistTrash;
    }
}
