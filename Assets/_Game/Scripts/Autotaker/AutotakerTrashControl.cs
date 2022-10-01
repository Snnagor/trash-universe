using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutotakerTrashControl : PlatformTrashControl
{
    [SerializeField] private AutotakerControl autotakerControl;
    [SerializeField] private ResourcePlace resourcePlace;
    [SerializeField] private PlatformSmallTrashMove smallPlatformMove;
    [SerializeField] private AutotakerAnim autotakerAnim;
    [SerializeField] private int countTrashTakeOneTime;

    private MainTrash _mainTrash;

    public int CountResourcesInOneMainTrash { get; set; }
    public int CountResourceInOneTake { get; set; }

    private int _countTake;
    private int _currentCountTake;
    private int _idCurrentTypeTrash;
    private int _idCurrentLocalPlatform;

    public override void Start()
    {

    }

    public override void SpawnTrash()
    {
        if (_serialDataManager.Data.SavedAutotakerTrash.Count > 0)
        {
            _idCurrentLocalPlatform = _serialDataManager.Data.SavedAutotakerTrash[0].idCurrentPlatform;

            CountResourcesInOneMainTrash = _gameManager.GetCurrentLoadCountResourcesAuto(_idCurrentLocalPlatform) /
                                           pointsSpawn.Length;
        }
        else
        {
            _idCurrentLocalPlatform = _gameManager.IdCurrentPlatform;
            CountResourcesInOneMainTrash = _gameManager.GetCurrentCountResourcesAuto() / pointsSpawn.Length;
        }

        SettingsSpawnTrash(CountResourcesInOneMainTrash);

        AllMainTrash[0].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        _mainTrash = AllMainTrash[0].GetComponent<MainTrash>();

        _countTake = Mathf.CeilToInt((float)_mainTrash.GetCountAllPiece() / countTrashTakeOneTime);
        CountResourceInOneTake = Mathf.RoundToInt(CountResourcesInOneMainTrash / _countTake);

        _currentCountTake = 0;

        LoadResources();
        ClearSaveDataTrash();
    }

    public override void SettingsSpawnTrash(int countResourcesInOneMainTrash)
    {
        TrashData newMainTrash = null;

        int currentTypeTrash = _gameManager.GetIdCurrentLoadTrash(_idCurrentLocalPlatform);

        for (int i = 0; i < pointsSpawn.Length; i++)
        {
            newMainTrash = CreateNewTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i);

            CurrentTrash++;
            AllMainTrash.Add(newMainTrash);
        }
    }

    public override TrashData CreateNewTrash(int countResourcesInOneMainTrash, TrashData newMainTrash,
        int currentTypeTrash, int i)
    {
        float rotation;

        if (_serialDataManager.Data.SavedAutotakerTrash.Count > 0)
        {
            rotation = _serialDataManager.Data.SavedAutotakerTrash[i].savedRotation;

            newMainTrash = ChangeTypeTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i, rotation);

            bool[] tmpListPieces = _serialDataManager.Data.SavedAutotakerTrash[i].allPieces;

            newMainTrash.LoadAllPieces(tmpListPieces);
        }
        else
        {
            newMainTrash = base.CreateNewTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i);
        }

        return newMainTrash;
    }

    public void TakeTrash()
    {
        _idCurrentTypeTrash = _gameManager.GetIdCurrentLoadTrash(_idCurrentLocalPlatform);
        _mainTrash.TakeTrash(countTrashTakeOneTime);
        _currentCountTake++;
    }

    public void OffTakeTrash(enumTypeTrash currentTypeTrash)
    {
        autotakerAnim.OffHand();
        StartMiningResource(currentTypeTrash);
    }

    public void OnTakeTrash()
    {
        if (IsThereTrash() && !autotakerControl.WorkAutotraker && smallPlatformMove.IsTherePlatform &&
            resourcePlace.IsTherePlace())
        {
            autotakerAnim.OnHand();
        }
        else if (!IsThereTrash())
        {
            smallPlatformMove.ChangePlatform();
        }
    }

    public bool IsThereTrash()
    {
        return _currentCountTake < _countTake;
    }

    public bool IsMiningResource()
    {
        return CountResourceInOneTake > autotakerControl.CurrentCreateResource;
    }

    public void StartMiningResource(enumTypeTrash currentTypeTrash)
    {
        autotakerControl.StartMiningResource(currentTypeTrash);
    }

    public void ChangePlatform()
    {
        if (!IsThereMainTrash)
        {
            smallPlatformMove.ChangePlatform();
            IsThereMainTrash = true;
        }
    }

    public enumTypeTrash GetTypeTrash()
    {
        return _mainTrash.TypeTrash;
    }

    public int GetCurrentCountPieces()
    {
        return _mainTrash.GetCurrentCountPieces();
    }

    public override void NewTrashSignal()
    {
        CurrentTrash = 0;
        ClearTrash();
        SpawnTrash();
    }


    public override void ForSaveDataTrash(TrashData trashData)
    {
        PreparationsForSaveAutotakerTrash(trashData);
    }

    private void PreparationsForSaveAutotakerTrash(TrashData trashData)
    {
        AutotakerTrashDataForSave saveData = new AutotakerTrashDataForSave(trashData.RotationToSave,
            trashData.TmpListForSave,
            _idCurrentLocalPlatform,
            resourcePlace.AllTypeResourcesInPlace.ToArray(),
            autotakerControl.CurrentCreateResource,
            _currentCountTake,
            _idCurrentTypeTrash);

        _serialDataManager.Data.SavedAutotakerTrash.Add(saveData);
    }

    public void SaveAutotakerTrahDestroy()
    {
        _serialDataManager.ClearDataSavedAutotakerTrash();

        for (int i = 0; i < AllMainTrash.Count; i++)
        {
            ForSaveDataTrash(AllMainTrash[i]);
        }
    }

    public override void ClearSaveDataTrash()
    {
        _serialDataManager.ClearDataSavedAutotakerTrash();
    }

    public override void LoadResources()
    {
        int countResourcesInPlace = _serialDataManager.Data.SavedAutotakerTrash[0].allResourcesInPlace.Length;

        LoadProgressDestroyMainTrash();

        if (countResourcesInPlace > 0)
        {
            autotakerAnim.OnWork();

            _idCurrentTypeTrash = _serialDataManager.Data.SavedAutotakerTrash[0].idCurrentCreateTypeResource;

            autotakerControl.LoadResourceFormSave(_serialDataManager.Data.SavedAutotakerTrash[0].createCurrentResource,
                _serialDataManager.Data.SavedAutotakerTrash[0].allResourcesInPlace,
                _idCurrentTypeTrash);
        }
    }

    public override void LoadProgressDestroyMainTrash()
    {
        _currentCountTake = _serialDataManager.Data.SavedAutotakerTrash[0].currentCountTake;
    }
}