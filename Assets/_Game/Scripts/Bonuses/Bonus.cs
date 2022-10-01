using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private Sprite icon;

    [TextArea]
    [SerializeField] private string nameBonus;
    [SerializeField] private int costBonus;
    [SerializeField] protected float timeBonus;
    [SerializeField] private BonusMove bonusPrefab;
    [SerializeField] private ImageTimer iconTimer;

    private BonusesManager bonusesManager;

    public BonusMove BonusPrefab { get => bonusPrefab; }
    public string NameBonus { get => nameBonus; }
    public Sprite Icon { get => icon; }
    public int CostBonus { get => costBonus; }

    protected float currentTimeBonus = -2;

    #region Injects

    protected SignalBus _signalBus;
    protected PurchaseControl _purchaseControl;

    [Inject]
    private void Construct(SignalBus signalBus,
                           PurchaseControl purchaseControl)
    {
        _signalBus = signalBus;
        _purchaseControl = purchaseControl;
    }

    #endregion

    private void Awake()
    {
        bonusesManager = GetComponent<BonusesManager>();
    }

    public virtual void Init()
    {
        currentTimeBonus = timeBonus;
        if (iconTimer == null) return;
        iconTimer.gameObject.SetActive(true);
        iconTimer.StartTimer(timeBonus);

        if (bonusesManager.PauseBonus)
            bonusesManager.PauseBonus = false;
    }

    public abstract void End();

    public abstract bool IsPurchasingThisBoost();

    public void Execute()
    {
        TimerBonuse();
    }

    private void TimerBonuse()
    {
        if (currentTimeBonus > 0)
        {
            if (!bonusesManager.PauseBonus)
                currentTimeBonus -= Time.deltaTime;
        }
        else if (currentTimeBonus > -1)
        {
            End();
            currentTimeBonus = -2;
        }
    }
}

