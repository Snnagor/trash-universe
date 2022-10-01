using UnityEngine;
using Zenject;

public class IndicatorTrashInPlatform : MonoBehaviour
{
    [SerializeField] private GameObject panelIndicatorTrash;
    public int CountTookResources { get; set; }

    #region Injects

    private ViewModel _viewModel;
    private GameManager _gameManager;
    private SignalBus _signalBus;
    private PlatformMainTrashControl _platformMainTrashControl;

    [Inject]
    private void Construct(ViewModel viewModel,
                           GameManager gameManager,
                           SignalBus signalBus,
                           PlatformMainTrashControl platformMainTrashControl)
    {
        _viewModel = viewModel;
        _gameManager = gameManager;
        _signalBus = signalBus;
        _platformMainTrashControl = platformMainTrashControl;
    }
    #endregion

    #region Signals

    private void OnEnable()
    {       
        _signalBus.Subscribe<TookPackedTrashSignal>(TookPackedTrashSignal);        
        _signalBus.Subscribe<MainTrashPlatformArrivalSignal>(MainTrashPlatformArrivalSignal);        
        _signalBus.Subscribe<NewPlatformSignal>(NewPlatformSignal);        
    }

    private void OnDisable()
    {      
        _signalBus.Unsubscribe<TookPackedTrashSignal>(TookPackedTrashSignal);        
        _signalBus.Unsubscribe<MainTrashPlatformArrivalSignal>(MainTrashPlatformArrivalSignal);        
        _signalBus.Unsubscribe<NewPlatformSignal>(NewPlatformSignal);        
    }

    private void Start()
    {
        panelIndicatorTrash.SetActive(false);
    }

    private void TookPackedTrashSignal()
    {
        CountTookResources++;

        UpdateUIFullTrailer();
    }

    private void MainTrashPlatformArrivalSignal()
    {
        panelIndicatorTrash.SetActive(true);
        
        
    }

    private void NewPlatformSignal()
    {
        panelIndicatorTrash.SetActive(false);
        CountTookResources = 0;
        _viewModel.TrashInPlatform = 1;
    }

    #endregion

    public void UpdateUIFullTrailer()
    {
        
        float percentFull =  1 - ((float)CountTookResources / _platformMainTrashControl.CountMaxPeacesInPlatform);

        _viewModel.TrashInPlatform = percentFull;
    }
}
