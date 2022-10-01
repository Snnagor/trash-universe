using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeSpeedCrusher : UIUpgradeContoller
{
    public override void BtnUpgrade()
    {
        if (!CanUpgradeCount()) return;

        if (!CanUpgradeCost()) 
        {
            _uIController.ShopPanel();
            return;
        }

        _upgradeSpeedCrusher.ChangeSpeed();
        _upgradeForceCrusher.ChangeForceCrusher();

        base.BtnUpgrade();
        _serialDataManager.Data.IdUpdateSpeedStar = currentLineUpgrade;
    }

    public override int GetCostUpgrade(int idUpgrade)
    {
        if (idUpgrade == _settings.CostSpeedStar.Length) return 0;
        return _settings.CostSpeedStar[idUpgrade];
    }

    public override void UpdatePriceToButton(int price)
    {
        _viewModelUpgrade.CostSpeedStar = price.ToString() + "$";
    }

    public override void LoadUpgradeData()
    {
        int idUpdateSpeedStar = _serialDataManager.Data.IdUpdateSpeedStar;

        if (idUpdateSpeedStar > 0)
        {
            for (int i = 0; i < idUpdateSpeedStar; i++)
            {
                _upgradeSpeedCrusher.ChangeSpeed();
                _upgradeForceCrusher.ChangeForceCrusher();
                AddLineUpgrade();
            }
        }
    }

    public override void MaxUpgardeButton()
    {
        if (!CanUpgradeCount())
        {
            _viewModelUpgrade.CostSpeedStar = "max";
        }
    }
}
