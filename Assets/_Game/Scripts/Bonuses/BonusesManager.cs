using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BonusesManager : MonoBehaviour
{
    [SerializeField] private Bonus[] bonuses;
    [SerializeField] private int countCraetedResourceBetweenBonuses = 20;
    [SerializeField] private float timeDestroyBonus = 7f;
    [SerializeField] private UIPanelBonusMove uiPanelMove;
    [SerializeField] private StateBonusManager stateBonusManager;
    [SerializeField] private ViewModelBonus _viewModelBonus;

    public Bonus CurrentBestBonus { get; set; }

    private int currentCraetedResourceBetweenBonuses;
    private int indexBonus;
    private int activeIndexBonus;

    public bool FirstBonus { get; set; }
    public bool PauseBonus { get; set; }

    #region Injects

    private SignalBus _signalBus;
    private DiContainer _diContainer;
    private SerialDataManager _serialDataManager;

    [Inject]
    private void Construct(SignalBus signalBus,
                           DiContainer diContainer,
                           SerialDataManager serialDataManager)
    {
        _signalBus = signalBus;
        _diContainer = diContainer;
        _serialDataManager = serialDataManager;
    }

    #endregion

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<BonusOpenUISignal>(BonusOpenUISignal);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<BonusOpenUISignal>(BonusOpenUISignal);
    }

    private void BonusOpenUISignal(BonusOpenUISignal bonus)
    {
        activeIndexBonus = bonus.IndexBonus;

        if (!bonuses[activeIndexBonus].IsPurchasingThisBoost())
        {
            uiPanelMove.ShowPanel();

            PauseBonus = true;
        }
        else
        {
            bonuses[activeIndexBonus].Init();
        }
    }

    #endregion

    public void CreatePackedTrash(Vector3 pos, Transform parent)
    {
        currentCraetedResourceBetweenBonuses++;

        if (currentCraetedResourceBetweenBonuses == countCraetedResourceBetweenBonuses)
        {
            CreateBonus(pos, parent);
            currentCraetedResourceBetweenBonuses = 0;
        }
    }

    private Bonus GetRandomBonus()
    {
        indexBonus = Random.Range(0, bonuses.Length);
        return bonuses[indexBonus];
    }

    private Bonus BestBonus()
    {
        CurrentBestBonus = stateBonusManager.GetBestBonus();

        return CurrentBestBonus;
    }

    public void CreateBonus(Vector3 pos, Transform parent)
    {
        Vector3 position = new Vector3(pos.x, pos.y + 1f, pos.z);
        BonusMove newBonus = Instantiate(BestBonus().BonusPrefab, position, Quaternion.identity, parent);

        _diContainer.InjectGameObject(newBonus.gameObject);

        newBonus.IndexBonus = Array.IndexOf(bonuses, CurrentBestBonus);

        if (!CurrentBestBonus.IsPurchasingThisBoost())
            UpdateUIInfo(CurrentBestBonus.Icon, CurrentBestBonus.NameBonus, CurrentBestBonus.CostBonus);

        Destroy(newBonus.gameObject, timeDestroyBonus);
    }

    private void Update()
    {
        ExecuteBonus();
    }

    public void InitActiveBonus()
    {
        if (CurrentBestBonus == null) return;
        CurrentBestBonus.Init();
    }

    private void ExecuteBonus()
    {
        foreach (var item in bonuses)
        {
            item.Execute();
        }
    }

    private void UpdateUIInfo(Sprite icon, string nameBonus, int costBonus)
    {
        _viewModelBonus.CostBonus = costBonus.ToString();
        _viewModelBonus.NameBonus = nameBonus;
        _viewModelBonus.SetIconBonus(icon);
    }
}
