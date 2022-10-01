using System.Collections.Generic;
using UnityEngine;

public class PlatformMainTrashControl : PlatformTrashControl
{
    private int createdTotalPackedTrash;
    public int CreatedPackedTrash { get; set; }
    private int idCurrentTypeTrash;
    private bool emptyPlatform;

    private List<TrashPackedData> loadResourcesList = new List<TrashPackedData>();

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<CreatePackedTrashSignal>(CreatePackedTrashSignal);
        _signalBus.Subscribe<TookPackedTrashSignal>(TookPackedTrashSignal);
        _signalBus.Subscribe<FindingPlatformSignal>(FindingPlatformSignal);
        _signalBus.Subscribe<MainTrashPlatformArrivalSignal>(StartTrashNullButtonOn);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<CreatePackedTrashSignal>(CreatePackedTrashSignal);
        _signalBus.Unsubscribe<TookPackedTrashSignal>(TookPackedTrashSignal);
        _signalBus.Unsubscribe<FindingPlatformSignal>(FindingPlatformSignal);
        _signalBus.Unsubscribe<MainTrashPlatformArrivalSignal>(StartTrashNullButtonOn);
    }

    private void CreatePackedTrashSignal()
    {
        CreatedPackedTrash++;     
        createdTotalPackedTrash++;
    }

    private void TookPackedTrashSignal()
    {
        CreatedPackedTrash--;

        if (CreatedPackedTrash == 0 && !IsThereMainTrash && !emptyPlatform)
        {
            if (!emptyPlatform)
                emptyPlatform = true;

            _newPlaformButton.SetButtonOn();
        }

        CheckTrashInPlatform();
    }

    private void CheckTrashInPlatform()
    {
        if (CreatedPackedTrash <= GetMaxPercentResources() && !IsThereMainTrashForButton)
        {
            _newPlaformButton.SetButtonOn();
        }
    }

    private void FindingPlatformSignal(FindingPlatformSignal isHere)
    {
        if (emptyPlatform && !isHere.value)
        {
            //// Улетает

            _signalBus.Fire(new NewPlatformSignal());
            _scoreManager.TotalHard += _settings.CountHardForCompleteCleaning;
        }
        
    }

    #endregion

    private int GetMaxPercentResources()
    {        
        int tmp = Mathf.RoundToInt(createdTotalPackedTrash * (100 - _settings.MaxPercentTrashes) * 0.01f);

        return tmp;
    }

    public override void NewTrashSignal()
    {
        ClearLoadResourcesList();

        base.NewTrashSignal();

        emptyPlatform = false;       

        CreatedPackedTrash = 0;
        createdTotalPackedTrash = 0;
        IsThereMainTrashForButton = true;
        IsThereMainTrash = true;
    }

    public void StartTrashNullButtonOn()
    {
        if (currentTrashForButton == 0)
        {
            IsThereMainTrashForButton = false;
        }

        if (!IsThereMainTrashForButton && CreatedPackedTrash == 0)
        {
            _newPlaformButton.StartButtonOn();
        }

        CheckTrashInPlatform();
    }

    public void StartTrashNullNewTrash()
    {
        if (CurrentTrash == 0)
        {
            IsThereMainTrash = false;
        }

        if (!IsThereMainTrash && CreatedPackedTrash == 0)
        {            
            NewTrashSignal();
        }
    }

    private void ClearLoadResourcesList()
    {
        if (loadResourcesList.Count > 0)
        {
            foreach (var item in loadResourcesList)
            {
                if (!item.GetIsCollected() && item.gameObject.activeSelf && item.GetIsOnGround())
                {
                    item.DeactivateForCleaning();
                }
            }

            loadResourcesList.Clear();
        }

        ClearSaveDataTrash();
    }

    public override TrashData CreateNewTrash(int countResourcesInOneMainTrash, TrashData newMainTrash, int currentTypeTrash, int i)
    {
        float rotation;

        idCurrentTypeTrash = currentTypeTrash;

        if (_serialDataManager.Data.SavedMainTrash.Count > 0)
        {
            rotation = _serialDataManager.Data.SavedMainTrash[i].savedRotation;

            newMainTrash = ChangeTypeTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i, rotation);

            bool[] tmpListPieces = _serialDataManager.Data.SavedMainTrash[i].allPieces;

            newMainTrash.LoadAllPieces(tmpListPieces);
        }
        else
        {
            newMainTrash = base.CreateNewTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i);
        }

        return newMainTrash;
    }

    public override void ForSaveDataTrash(TrashData trashData)
    {
        PreparationForSaveMainTrash(trashData);
        PreparationForSaveNewResources(trashData);
    }

    private void PreparationForSaveMainTrash(TrashData trashData)
    {
        TrashDataForSave saveData = new TrashDataForSave(trashData.RotationToSave, trashData.TmpListForSave);
        _serialDataManager.Data.SavedMainTrash.Add(saveData);
    }

    private void PreparationForSaveNewResources(TrashData trashData)
    {
        trashData.FillListResourcesOnGround();

        if (trashData.ResourcesListOnGround.Count > 0)
        {
            foreach (var item in trashData.ResourcesListOnGround)
            {
                if (!item.GetIsCollected())
                {
                    ResourceDataForSave saveDataResource = new ResourceDataForSave(item.transform.localPosition, item.transform.eulerAngles);
                    _serialDataManager.Data.SavedResources.Add(saveDataResource);
                }
            }
        }
    }

    private void PreparationForSaveOldResources()
    {
        if (loadResourcesList.Count > 0)
        {
            foreach (var item in loadResourcesList)
            {
                if (!item.GetIsCollected())
                {
                    ResourceDataForSave saveDataResource = new ResourceDataForSave(item.transform.localPosition, item.transform.eulerAngles);
                    _serialDataManager.Data.SavedResources.Add(saveDataResource);
                }
            }
        }
    }

    public void SaveMainTrahDestroy()
    {
        ClearSaveDataTrash();

        for (int i = 0; i < AllMainTrash.Count; i++)
        {
            ForSaveDataTrash(AllMainTrash[i]);
        }

        _serialDataManager.Data.CreatedTotalPackedTrash = createdTotalPackedTrash;
        _serialDataManager.Data.EmptyPlatform = emptyPlatform;

        PreparationForSaveOldResources();

    }

    public override void ClearSaveDataTrash()
    {
        _serialDataManager.ClearDataSavedMainTrash();
    }

    public void LoadResourceFormSave()
    {
        var sevedResources = _serialDataManager.Data.SavedResources;

        for (int i = 0; i < sevedResources.Count; i++)
        {
            if(sevedResources[i].savedPosition[1] > 1f)
            {
                sevedResources[i].savedPosition[1] = 1f;
            }

            Vector3 position = new Vector3(sevedResources[i].savedPosition[0],
                                           sevedResources[i].savedPosition[1],
                                           sevedResources[i].savedPosition[2]);

            Vector3 rotation = new Vector3(sevedResources[i].savedRotation[0],
                                           sevedResources[i].savedRotation[1],
                                           sevedResources[i].savedRotation[2]);

            var newResource = _createResource.NewResource(position, idCurrentTypeTrash, PlaceCreatResources.LoadOnGround, transform);
            newResource.transform.localEulerAngles = rotation;
            loadResourcesList.Add(newResource);

            CreatedPackedTrash++;
        }
    }

    public override void LoadResources()
    {
        LoadResourceFormSave();
    }

    public override void LoadProgressDestroyMainTrash()
    {
        createdTotalPackedTrash = _serialDataManager.Data.CreatedTotalPackedTrash;
        emptyPlatform = _serialDataManager.Data.EmptyPlatform;
    }
}
