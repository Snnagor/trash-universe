using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeWheels : UIUpgradeContoller
{
    public override void Start()
    {
        
    }

    public override void BtnUpgrade()
    {
        if (!CanUpgradeCount()) return;

        if (!CanUpgradeCost())
        {
            _uIController.ShopPanel();
            return;
        }

        _upgradeWheels.ChangeWheels();

        base.BtnUpgrade();
        _serialDataManager.Data.IdUpdateWheels = currentLineUpgrade;
    }

    public override int GetCostUpgrade(int idUpgrade)
    {
        if (idUpgrade == _settings.CostSpeedWheels.Length) return 0;
        return _settings.CostSpeedWheels[idUpgrade];
    }

    public override void UpdatePriceToButton(int price)
    {
        _viewModelUpgrade.CostSpeedWheels = price.ToString() + "$";
    }

    public override void LoadUpgradeData()
    {
        int idUpdateWheels = _serialDataManager.Data.IdUpdateWheels;

        if (idUpdateWheels > 0)
        {
            for (int i = 0; i < idUpdateWheels; i++)
            {
                _upgradeWheels.ChangeWheels();
                AddLineUpgrade();
            }
        }
    }

    public override void MaxUpgardeButton()
    {
        if (!CanUpgradeCount())
        {
            _viewModelUpgrade.CostSpeedWheels = "max";
        }
    }
}
