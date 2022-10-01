using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class UIUpgradeContoller : MonoBehaviour
{
    [SerializeField] protected GameObject[] lineUpgrade;

    protected int currentLineUpgrade;

    #region Injects
   
    protected UpgradeLadle _upgradeLadle;
    protected UpgradeSizeStars _upgradeSize;
    protected UpgradeSpeedCrasher _upgradeSpeedCrusher;
    protected UpgradeTrailer _upgradeTrailer;
    protected UpgradeWheels _upgradeWheels;
    protected UpgradeForceCrusher _upgradeForceCrusher;
    protected ViewModelUpgrade _viewModelUpgrade;
    protected Settings _settings;
    protected ScoreManager _scoreManager;
    protected SerialDataManager _serialDataManager;
    protected UIController _uIController;

    [Inject]
    private void Construct(UpgradeLadle upgradeLadle,
                           UpgradeSizeStars upgradeSize,
                           UpgradeSpeedCrasher upgradeSpeedCrusher,
                           UpgradeTrailer upgradeTrailer,
                           UpgradeWheels upgradeWheels,
                           UpgradeForceCrusher upgradeForceCrusher,
                           ViewModelUpgrade viewModelUpgrade,
                           Settings settings,
                           ScoreManager scoreManager,
                           SerialDataManager serialDataManager,
                           UIController uIController)
    {       
        _upgradeLadle = upgradeLadle;
        _upgradeSize = upgradeSize;
        _upgradeSpeedCrusher = upgradeSpeedCrusher;
        _upgradeTrailer = upgradeTrailer;
        _upgradeWheels = upgradeWheels;
        _upgradeForceCrusher = upgradeForceCrusher;
        _viewModelUpgrade = viewModelUpgrade;
        _settings = settings;
        _scoreManager = scoreManager;
        _serialDataManager = serialDataManager;
        _uIController = uIController;
    }

    #endregion

    public virtual void Start()
    {
        Init();
    }


    public void Init()
    {        
        LoadUpgradeData();
        
        UpdatePriceToButton(GetCostUpgrade(currentLineUpgrade));

        MaxUpgardeButton();
    }

    public abstract void MaxUpgardeButton();

    public abstract int GetCostUpgrade(int idUpgrade);

    public abstract void UpdatePriceToButton(int price);

    public abstract void LoadUpgradeData();

    public virtual void BtnUpgrade()
    {                
        UpdateLineUpgrade();
        MaxUpgardeButton();
    }

    public void UpdateLineUpgrade()
    {
        SpendMoney(GetCostUpgrade(currentLineUpgrade));

        AddLineUpgrade();
        UpdatePriceToButton(GetCostUpgrade(currentLineUpgrade));
    }

    public void AddLineUpgrade()
    {       
        lineUpgrade[currentLineUpgrade].SetActive(true);
        currentLineUpgrade ++;
    }

    public bool CanUpgradeCount()
    {
        if (currentLineUpgrade == lineUpgrade.Length) return false;

        return true;
    }

    public bool CanUpgradeCost()
    {
        if (_scoreManager.TotalMoney < GetCostUpgrade(currentLineUpgrade)) return false;

        return true;
    }

    public void SpendMoney(int valueMoney)
    {        
        _scoreManager.TmpMoney -= valueMoney;
    }
       
}
