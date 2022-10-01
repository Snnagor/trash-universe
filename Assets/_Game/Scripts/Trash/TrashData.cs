using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrashData : ItemForSpawn
{
    private MainTrash mainTrash;

    public bool[] TmpListForSave { get; set; }
    public List<TrashPackedData> ResourcesListOnGround { get; set; } = new List<TrashPackedData>();

    public float RotationToSave { get; set; }

    #region Injects

    private SerialDataManager _serialDataManager;       

    [Inject]
    private void Construct(SerialDataManager serialDataManager)
    {
        _serialDataManager = serialDataManager;
    }

    #endregion

    private void Awake()
    {
        mainTrash = GetComponent<MainTrash>();

        TmpListForSave = new bool[mainTrash.GetCountAllPiece()];
    }

    public void Deactivate()
    {
        mainTrash.CleanResourcesList();
        transform.SetParent(RootParent);
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void OnSpawnedPool(Vector3 pos, Transform parent, int countResources, float rotation)
    {
        transform.position = pos;

        transform.SetParent(parent);
        transform.localScale = new Vector3(1f, 1f, 1f);
        mainTrash.TrashControl = parent.GetComponent<PlatformTrashControl>();

        mainTrash.CountResourceFromPiece(countResources);
        RotationToSave = rotation;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);

        mainTrash.SetSettings();
    }   

    public void InitFillTmpListForSave()
    {
        for (int i = 0; i < TmpListForSave.Length; i++)
        {
            TmpListForSave[i] = true;
        }
    }

    public void LoadAllPieces(bool[] tmpAllPieces)
    {
        for (int i = 0; i < TmpListForSave.Length; i++)
        {
            TmpListForSave[i] = tmpAllPieces[i];
            mainTrash.AllPieces[i].gameObject.SetActive(tmpAllPieces[i]);

            if(tmpAllPieces[i] == false)
                mainTrash.CurrentCountPieces--;
        }
    }

    public bool IsTherePeaces()
    {
        return mainTrash.CurrentCountPieces > 0;
    }
    public int GetCurrentCountPeaces()
    {
        return mainTrash.GetCurrentCountPieces();
    }

    public int GetCountAllPiece()
    {
        return mainTrash.GetCountAllPiece();
    }

    public float GetResist()
    {
        return mainTrash.Resist;
    }

    public bool IsTherePeacesForButton()
    {
       return mainTrash.CurrentCountPieces > mainTrash.MaxPercentPieces;
    }

    public void FillListResourcesOnGround()
    {
        if (ResourcesListOnGround.Count > 0) ResourcesListOnGround.Clear();

        foreach (var item in mainTrash.ResourceList)
        {
            if (item.GetIsOnGround())
            {
                ResourcesListOnGround.Add(item);
            }
        }
    }
}
