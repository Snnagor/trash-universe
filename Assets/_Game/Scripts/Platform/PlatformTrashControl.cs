using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PlatformTrashControl : MonoBehaviour
{
    [SerializeField] protected Transform[] pointsSpawn;
    [SerializeField] protected Transform parentContainer;
        
    public List<TrashData> AllMainTrash { get; set; } = new List<TrashData>();

    public int CurrentTrash { get; set; }
    protected int currentTrashForButton;

    public bool IsThereMainTrash { get; set; } = true;
    public bool IsThereMainTrashForButton { get; set; } = true;
    public int CountMaxPeacesInPlatform { get; set; }

    #region Injects

    protected SignalBus _signalBus;
    protected CreateTrashBottle _createTrashBottle;
    protected CreateTrashPaper _createTrashPaper;
    protected CreateTrashCardboard _createTrashCardboard;
    protected CreateTrashTire _createTrashTire;
    protected GameManager _gameManager;
    protected SerialDataManager _serialDataManager;
    protected CreateResource _createResource;
    protected PlatformNewButton _newPlaformButton;
    protected ScoreManager _scoreManager;
    protected Settings _settings;

    [Inject]
    private void Construct(SignalBus signalBus,
                           CreateTrashBottle createTrashBottle,
                           CreateTrashPaper createTrashPaper,
                           CreateTrashTire createTrashTire,
                           CreateTrashCardboard createTrashCardboard,
                           GameManager gameManager,
                           SerialDataManager serialDataManager,
                           CreateResource createResource,
                           PlatformNewButton newPlaformButton,
                           ScoreManager scoreManager,
                           Settings settings)
    {
        _signalBus = signalBus;
        _createTrashBottle = createTrashBottle;
        _createTrashPaper = createTrashPaper;
        _createTrashTire = createTrashTire;
        _createTrashCardboard = createTrashCardboard;
        _gameManager = gameManager;
        _serialDataManager = serialDataManager;
        _createResource = createResource;
        _newPlaformButton = newPlaformButton;
        _scoreManager = scoreManager;
        _settings = settings;
    }

    #endregion

    public virtual void Start()
    {
        SpawnTrash();
    }

    public virtual void MainTrashEmptySignal()
    {
        CurrentTrash--;
      
        if (CurrentTrash == 0)
        {
            IsThereMainTrash = false;
        }
    }

    public virtual void MainTrashEmptySignalForButton()
    {
        currentTrashForButton--;

        if (currentTrashForButton == 0)
        {           
            IsThereMainTrashForButton = false;
        }
    }

    public virtual void NewTrashSignal()
    {
        CurrentTrash = 0;

        ClearTrash();
        NextPlatform();
    }

    public void NextPlatform()
    {
        _gameManager.NextPlatform();
        SpawnTrash();
    }

    public virtual void SpawnTrash()
    {
        int countResourcesInOneMainTrash = _gameManager.GetCurrentCountResources() / pointsSpawn.Length;

        SettingsSpawnTrash(countResourcesInOneMainTrash);
    }

    public virtual void SettingsSpawnTrash(int countResourcesInOneMainTrash)
    {
        TrashData newMainTrash = null;

        int currentTypeTrash = _gameManager.GetIdCurrentTrash();

        CountMaxPeacesInPlatform = 0;

        for (int i = 0; i < pointsSpawn.Length; i++)
        {
            newMainTrash = CreateNewTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i);

            if (newMainTrash.IsTherePeaces())
                CurrentTrash++;
            
            if (newMainTrash.IsTherePeacesForButton())
                currentTrashForButton++;

            CountMaxPeacesInPlatform += newMainTrash.GetCountAllPiece();

            AllMainTrash.Add(newMainTrash);
        }   
        
        LoadProgressDestroyMainTrash();
        LoadResources();
        ClearSaveDataTrash();
    }

    public virtual TrashData CreateNewTrash(int countResourcesInOneMainTrash, TrashData newMainTrash, int currentTypeTrash, int i)
    {
        float rotation = Random.Range(0, 180f);

        newMainTrash = ChangeTypeTrash(countResourcesInOneMainTrash, newMainTrash, currentTypeTrash, i, rotation);

        newMainTrash.InitFillTmpListForSave();

        return newMainTrash;
    }

    public TrashData ChangeTypeTrash(int countResourcesInOneMainTrash, TrashData newMainTrash, int currentTypeTrash, int i, float rotation)
    {
        switch (currentTypeTrash)
        {
            case 0: newMainTrash = _createTrashPaper.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
            case 1: newMainTrash = _createTrashCardboard.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
            case 2: newMainTrash = _createTrashBottle.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
            case 3: newMainTrash = _createTrashTire.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
        }

        return newMainTrash;
    }

    public abstract void ForSaveDataTrash(TrashData trashData);

    public abstract void ClearSaveDataTrash();

    public abstract void LoadResources();
    public abstract void LoadProgressDestroyMainTrash();

    public void RandomSpawnTrash()
    {
        TrashData newMainTrash = null;

        int randTrash = Random.Range(0, 3);

        int countResourcesInOneMainTrash = _gameManager.GetCurrentCountResources() / pointsSpawn.Length;

        for (int i = 0; i < pointsSpawn.Length; i++)
        {
            float rotation = Random.Range(0, 180f);

            switch (randTrash)
            {
                case 0: newMainTrash = _createTrashPaper.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
                case 1: newMainTrash = _createTrashCardboard.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
                case 2: newMainTrash = _createTrashBottle.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
                case 3: newMainTrash = _createTrashTire.NewItem(pointsSpawn[i].position, parentContainer, countResourcesInOneMainTrash, rotation); break;
            }

            CurrentTrash++;

            AllMainTrash.Add(newMainTrash);
        }
    }

    public void ClearTrash()
    {
        foreach (var item in AllMainTrash)
        {
            item.Deactivate();
        }

        AllMainTrash.Clear();
    }

    public int GetCountAllMainTrash()
    {
        return AllMainTrash.Count;
    }
    
    public int GetTotalPieceOneTrash()
    {
        return AllMainTrash[0].GetCountAllPiece();
    }
}
