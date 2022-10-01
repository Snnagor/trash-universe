using Zenject;

public class BonusMagnet : Bonus
{    
    #region Injects

    private PlayerCleaning _playerCleaning;

    [Inject]
    private void Construct(PlayerCleaning playerCleaning)
    {
        _playerCleaning = playerCleaning;
    }

    #endregion

    public override void Init()
    {
        base.Init();
        _playerCleaning.EnableMagnetBonusCollider(true);
    }      

    public override void End()
    {
        _playerCleaning.EnableMagnetBonusCollider(false);
    }

    public override bool IsPurchasingThisBoost()
    {
        return _purchaseControl.MagnetBoost;
    }
}
