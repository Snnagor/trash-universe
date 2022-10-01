using UnityEngine;
using Zenject;
using Zenject;

[RequireComponent(typeof(ViewModelBase))]
public class PlatformManager : MonoBehaviour
{
    [SerializeField] private int costRecyclingPlatform;
    public int CostRecyclingPlatform { get => costRecyclingPlatform; }

    [SerializeField] private int costAutoTakerPlatform;
    public int CostAutoTakerPlatform { get => costAutoTakerPlatform; }

    [SerializeField] private int costNextLevel;
    public int CostNextLevel { get => costNextLevel; }

    [SerializeField] private FrontWall frontWall;

    private ViewModelBase viewModelBase;

    private int currentRecyclingPlatformMoney;
    public int CurrentRecyclingPlatformMoney 
    { 
        get 
        {
            return currentRecyclingPlatformMoney;
        }

        set
        {
            currentRecyclingPlatformMoney = value;

            viewModelBase.CurrentCostRecycling = currentRecyclingPlatformMoney.ToString();
        }
    }

    private int currentAutoTakerPlatformMoney;
    public int CurrentAutotakerPlatformMoney
    {
        get
        {
            return currentAutoTakerPlatformMoney;
        }

        set
        {
            currentAutoTakerPlatformMoney = value;
            
            viewModelBase.CurrentCostAutotaker = currentAutoTakerPlatformMoney.ToString();
        }
    }

    private int currentNextLevelMoney;
    public int CurrentNextLevelMoney
    {
        get
        {
            return currentNextLevelMoney;
        }

        set
        {
            currentNextLevelMoney = value;

            viewModelBase.CurrentCostNextLevel = currentNextLevelMoney.ToString();
        }
    }

    #region Injects

    private ScoreManager _scoreManager;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(ScoreManager scoreManager,
                           SignalBus signalBus)
    {
        _scoreManager = scoreManager;
        _signalBus = signalBus;
    }

    #endregion

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<MainTrashPlatformArrivalSignal>(OpenFrontWall);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MainTrashPlatformArrivalSignal>(OpenFrontWall);
    }

    #endregion

    private void Awake()
    {
        viewModelBase = GetComponent<ViewModelBase>();
    }

    private void Start()
    {
        viewModelBase.CostRecyclig = CostRecyclingPlatform.ToString();
        viewModelBase.CostAutotaker = CostAutoTakerPlatform.ToString();
        viewModelBase.CostNextLevel = CostNextLevel.ToString();
    }

    public void OpenFrontWall()
    {
        frontWall.OpenFrontWall();
    }

    public void CloseFrontWall()
    {
        frontWall.CloseFrontWall();
    }
}
