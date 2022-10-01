using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public enum enumTypeTrash
{
    paper = 0,
    cardboard = 1,
    bottle = 2,
    tire = 3
}

public class MainTrash : MonoBehaviour
{
    [SerializeField] private enumTypeTrash typeTrash;
    [SerializeField] private DestroyTrash[] allPieces;
    public DestroyTrash[] AllPieces { get => allPieces; set => allPieces = value; }
    [SerializeField] private int maxCreateResourceFromOnePiece;
    [Space]
    [Header("Peace Settings")]
    [SerializeField] private int health;
    [SerializeField] private float resist;
    public float Resist { get => resist; set => resist = value; }

    private TrashData trashData;

    private List<TrashPackedData> resourceList = new List<TrashPackedData>();
    public List<TrashPackedData> ResourceList { get => resourceList; set => resourceList = value; }

    public int CurrentCountPieces { get; set; }
    private int countResourceFromOnePiece;

    public PlatformTrashControl TrashControl { get; set; }
    public int CountMaxResource { get; set; }
    public enumTypeTrash TypeTrash { get => typeTrash; }

    public int MaxPercentPieces { get; set; }
    public int MinPercentPieces { get; set; }
    

    #region Injects

    private SignalBus _signalBus;
    private CreateResource _createResource;
    private BonusesManager _bonusesManager;
    private GameManager _gameManager;
    private SoundManager _soundManager;
    private Settings _settings;

    [Inject]
    private void Construct(SignalBus signalBus,
                           CreateResource createResource,
                           BonusesManager bonusesManager,
                           GameManager gameManager,
                           SoundManager soundManager,
                           Settings settings)
    {
        _signalBus = signalBus;
        _createResource = createResource;
        _bonusesManager = bonusesManager;
        _gameManager = gameManager;
        _soundManager = soundManager;
        _settings = settings;
    }

    #endregion

    private void Awake()
    {
        trashData = GetComponent<TrashData>();
    }

    private void OnEnable()
    {
        CurrentCountPieces = AllPieces.Length;        
    }

    public void SetSettings()
    {
       foreach (var item in AllPieces)
        {
            item.SetSettings(health, Resist + _gameManager.CurrentIncreaseResisTrash);
        }

        SetPercentDestroyTrash();
    }

    public void SetPercentDestroyTrash()
    {
        MaxPercentPieces = Mathf.RoundToInt(AllPieces.Length * (100 - _settings.MaxPercentTrashes) * 0.01f);
        MinPercentPieces = Mathf.RoundToInt(AllPieces.Length * (100 - _settings.MinPercentTrashes) * 0.01f);
    }

    public void DestroyPiece(DestroyTrash item)
    {
        CurrentCountPieces--;
        ForSerialSave(item);

        if (!ChanceCountCreate())
        {
            CreateManyResource(item.transform.position);
        }
        else
        {
            CreateChanceResource(item.transform.position, countResourceFromOnePiece);
        }

        IsTherePeaces();
    }

    private void ForSerialSave(DestroyTrash item)
    {        
        int index = Array.IndexOf(AllPieces, item);
        trashData.TmpListForSave[index] = false;
    }

    private void IsTherePeaces()
    {       
        if (CurrentCountPieces == 0)
        {
           
            if (TrashControl != null)
                TrashControl.MainTrashEmptySignal();
        }

        if (CurrentCountPieces == MaxPercentPieces)
        {           
            if (TrashControl != null)
                TrashControl.MainTrashEmptySignalForButton();           
        }
    }

    private void CreateManyResource(Vector3 position)
    {
        Vector3 pos = new Vector3(position.x, position.y + 5.5f, position.z);

        for (int i = 0; i < countResourceFromOnePiece; i++)
        {
            CreateResource(pos);
        }

        if(countResourceFromOnePiece > 1)
        {
            int delta = countResourceFromOnePiece - 1;
            TrashControl.CountMaxPeacesInPlatform += delta;
        }
    } 

    private void CreateChanceResource(Vector3 position, int chance)
    {       
        int chanceCreatePacked = Random.Range(0, chance);

        if (chanceCreatePacked == 0)
        {
            Vector3 pos = new Vector3(position.x, position.y + 5.5f, position.z);
            CreateResource(pos);
        }
        else
        {
            TrashControl.CountMaxPeacesInPlatform--;
        }
    }

    private void CreateResource(Vector3 position)
    {
        var newResource = _createResource.NewResource(position, TypeTrash.GetHashCode(), PlaceCreatResources.DestroyTrash, transform.parent);

        ResourceList.Add(newResource);

        _bonusesManager.CreatePackedTrash(position, transform.parent);
        _signalBus.Fire(new CreatePackedTrashSignal());
    }

    public int GetTypeTrash()
    {
        return TypeTrash.GetHashCode();
    }

    public void CleanResourcesList()
    {
        if (ResourceList.Count > 0)
        {
            foreach (var item in ResourceList)
            {
                if (!item.GetIsCollected() && item.gameObject.activeSelf)
                {
                    item.DeactivateForCleaning();
                }
            }
        }

        ResourceList.Clear();


        foreach (var item in AllPieces)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void TakeTrash(int countTakeTrash)
    {
        for (int i = 0; i < countTakeTrash; i++)
        {
            CurrentCountPieces--;
            AllPieces[CurrentCountPieces].gameObject.SetActive(false);
            ForSerialSave(AllPieces[CurrentCountPieces]);

            if (CurrentCountPieces == 0)
            {               
                TrashControl.MainTrashEmptySignal();
                break;
            }
        }
    }

    public int GetCurrentCountPieces()
    {
        return CurrentCountPieces;
    }

    private bool ChanceCountCreate()
    {
        return AllPieces.Length > CountMaxResource; 
    }

    public void CountResourceFromPiece(int countMaxResource)
    {
        CountMaxResource = countMaxResource;

        if (!ChanceCountCreate())
        {
            countResourceFromOnePiece = CountMaxResource / AllPieces.Length;
        }
        else
        {
            countResourceFromOnePiece = Mathf.RoundToInt((float)AllPieces.Length / CountMaxResource);
        }
    }

    public int GetCountAllPiece()
    {
        return AllPieces.Length;
    }

    public void PlaySoundDestroyPieceTrash()
    {
        _soundManager.DestroyTrashOne();
    }

}
