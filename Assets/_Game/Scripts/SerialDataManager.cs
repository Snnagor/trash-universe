using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class RecyclingResourceDataForSave
{
    public int CountResource;
    public int CountProduct;
    public RecyclingResourceDataForSave(int CountResource, int SavedProductCount)
    {
        this.CountResource = CountResource;
        this.CountProduct = SavedProductCount;
    }
}

[Serializable]
public class TrashDataForSave
{
    public float savedRotation;
    public bool[] allPieces;

    public TrashDataForSave(float savedRotation, bool[] allPieces)
    {
        this.savedRotation = savedRotation;
        this.allPieces = allPieces;
    }
}

[Serializable]
public class AutotakerTrashDataForSave
{
    public float savedRotation;
    public bool[] allPieces;
    public int idCurrentPlatform;
    public int[] allResourcesInPlace;
    public int createCurrentResource;
    public int currentCountTake;
    public int idCurrentCreateTypeResource;

    public AutotakerTrashDataForSave(float savedRotation,
                                     bool[] allPieces,
                                     int idCurrentPlatform,
                                     int[] allResourcesInPlace,
                                     int createCurrentResource,
                                     int currentCountTake,
                                     int idCurrentCreateTypeResource)
    {
        this.savedRotation = savedRotation;
        this.allPieces = allPieces;
        this.idCurrentPlatform = idCurrentPlatform;
        this.allResourcesInPlace = allResourcesInPlace;
        this.createCurrentResource = createCurrentResource;
        this.currentCountTake = currentCountTake;
        this.idCurrentCreateTypeResource = idCurrentCreateTypeResource;
    }
}

[Serializable]
public class ResourceDataForSave
{
    public float[] savedPosition;
    public float[] savedRotation;

    public ResourceDataForSave(Vector3 savedPosition, Vector3 savedRotation)
    {
        this.savedPosition = new float[3];
        this.savedPosition[0] = savedPosition.x;
        this.savedPosition[1] = savedPosition.y;
        this.savedPosition[2] = savedPosition.z;

        this.savedRotation = new float[3];
        this.savedRotation[0] = savedRotation.x;
        this.savedRotation[1] = savedRotation.y;
        this.savedRotation[2] = savedRotation.z;
    }
}

[Serializable]
public class SaveData
{
    public bool FirstBonus;

    public bool TutorialControl = true;
    public bool TutorialTeleport = true;
    public bool TutorialRecyclingAutotaker = true;
    public bool TutorialUpgrade = true;
    public bool TutorialRecycling = true;
    public bool TutorialFullAutotaker = true;
    public bool TutorialNextLevel = true;
    public bool TutorialAutotakerFirstTime = true;

    public bool NoAdsBoost;

    public bool SpecialOffer;
    public bool OneTimeOffer;
    public bool JunkBotSet;
    public ulong LastClaimTime;
    public bool ForceBoost;
    public bool CrusherBoost;
    public bool TrailerBoost;
    public bool MagnetBoost;

    public bool Music = true;
    public bool Sound = true;
    public bool Vibro = true;

    public int TotalMoney;
    public int TotalHard;


   // public int CountTookResources;
    //Main Trash Platform
    public int IdCurrentPlatform;
    public float CurrentIncreaseResisTrash;
    public int CreatedTotalPackedTrash;
    public bool EmptyPlatform;

    // Built Buiding
    public bool BuiltRecyclilng;
    public int CurrentPaidMoneyRecycling;

    public bool BuiltAutotaker;
    public int CurrentPaidMoneyAutotaker;

    public bool BusArrival;
    public int CurrentPaidMoneyNextLevel;

    // Update
    public int IdUpdateCountStar;
    public int IdUpdateGroupStar;
    public int IdUpdateSizeStar;
    public int IdUpdateSpeedStar;
    public int IdUpdateTrailer;
    public int IdUpdateWheels;

    //Analytics
    public float CurrentSecondTime;
    public int CurrentMinuteTime;
    public int AnalyticsCountPlatform;
    public bool AnalyticsFirsPurchase;

    public List<AutotakerTrashDataForSave> SavedAutotakerTrash = new List<AutotakerTrashDataForSave>();

    public List<TrashDataForSave> SavedMainTrash = new List<TrashDataForSave>();
    public List<ResourceDataForSave> SavedResources = new List<ResourceDataForSave>();

    public List<int> SavesTypeResoursesTrailer = new List<int>();
    public List<int> SavesTypeProductTrailer = new List<int>();

    public List<RecyclingResourceDataForSave> SavedResourcesRecycling = new List<RecyclingResourceDataForSave>();
    public List<int> SavesProductTypeInPlace = new List<int>();
}

public class SerialDataManager
{
    public const string URL_SAVE_FILE = "/TrashWorldSaveData.dat";

    SaveData dataSave = new SaveData();
    public SaveData Data { get => dataSave; set => dataSave = value; } 

    public void SaveDataTrash()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + URL_SAVE_FILE);

        bf.Serialize(file, Data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + URL_SAVE_FILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + URL_SAVE_FILE, FileMode.Open);
            Data = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + URL_SAVE_FILE))
        {
            File.Delete(Application.persistentDataPath
              + URL_SAVE_FILE);

            RestartNewLevel();

            Data.FirstBonus = false;           

            Data.TutorialControl = true;
            Data.TutorialTeleport = true;
            Data.TutorialRecyclingAutotaker = true;
            Data.TutorialUpgrade = true;
            Data.TutorialRecycling = true;
            Data.TutorialFullAutotaker = true;
            Data.TutorialNextLevel = true;
            Data.TutorialAutotakerFirstTime = true;

            Data.SpecialOffer = false;
            Data.OneTimeOffer = false;
            Data.NoAdsBoost = false;

            Data.JunkBotSet = false;
            Data.LastClaimTime = 0;
            Data.ForceBoost = false;
            Data.CrusherBoost = false;
            Data.TrailerBoost = false;
            Data.MagnetBoost = false;

            //Analytics
            Data.CurrentSecondTime = 0;
            Data.CurrentMinuteTime = 0;
            Data.AnalyticsCountPlatform = 0;
            Data.AnalyticsFirsPurchase = false;
        }
        else
            Debug.LogError("No save data to delete.");
    }

    public void RestartNewLevel()
    {
        Data.TotalMoney = 0;
        Data.TotalHard = 0;
        Data.IdCurrentPlatform = 0;

        Data.CurrentIncreaseResisTrash = 0;
        Data.CreatedTotalPackedTrash = 0;
        Data.EmptyPlatform = false;

        Data.BuiltRecyclilng = false;
        Data.CurrentPaidMoneyRecycling = 0;

        Data.BuiltAutotaker = false;
        Data.CurrentPaidMoneyAutotaker = 0;

        Data.BusArrival = false;
        Data.CurrentPaidMoneyNextLevel = 0;


        Data.IdUpdateCountStar = 0;
        Data.IdUpdateGroupStar = 0;
        Data.IdUpdateSizeStar = 0;
        Data.IdUpdateSpeedStar = 0;
        Data.IdUpdateTrailer = 0;
        Data.IdUpdateWheels = 0;


        Data.SavedMainTrash.Clear();
        Data.SavedResources.Clear();

        Data.SavedAutotakerTrash.Clear();

        Data.SavesTypeResoursesTrailer.Clear();
        Data.SavesTypeProductTrailer.Clear();

        Data.SavedResourcesRecycling.Clear();
        Data.SavesProductTypeInPlace.Clear();
    }

    public void ClearDataSavedMainTrash()
    {
        if (Data.SavedMainTrash.Count > 0)
        {
            Data.SavedMainTrash.Clear();
        }

        if (Data.SavedResources.Count > 0)
        {
            Data.SavedResources.Clear();
        }

        Data.CreatedTotalPackedTrash = 0;
        Data.EmptyPlatform = false;
    }

    public void ClearDataSavedAutotakerTrash()
    {
        if (Data.SavedAutotakerTrash == null) return;

        if (Data.SavedAutotakerTrash.Count > 0)
        {
            Data.SavedAutotakerTrash.Clear();
        }
    }

    public void ClearDataSavesItemInTrailer()
    {
        Data.SavesTypeResoursesTrailer.Clear();
        Data.SavesTypeProductTrailer.Clear();
    }

    public void ClearDataSavesRecyclilng()
    {
        Data.SavedResourcesRecycling.Clear();
    }
}
