using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeSizeStar : UIUpgradeContoller 
{
    public override void BtnUpgrade()
    {
        if (!CanUpgradeCount()) return;

        if (!CanUpgradeCost())
        {
            _uIController.ShopPanel();
            return;
        }

        _upgradeSize.ChangeSize();

        base.BtnUpgrade();

        _serialDataManager.Data.IdUpdateSizeStar = currentLineUpgrade;
    }

    public override int GetCostUpgrade(int idUpgrade)
    {
        if (idUpgrade == _settings.CostSizeStar.Length) return 0;
        return _settings.CostSizeStar[idUpgrade];
    }

    public override void UpdatePriceToButton(int price)
    {
        _viewModelUpgrade.CostSizeStar = price.ToString() + "$";
    }
    public override void LoadUpgradeData()
    {
        int idUpdateSizeStar = _serialDataManager.Data.IdUpdateSizeStar;

        if (idUpdateSizeStar > 0)
        {
            for (int i = 0; i < idUpdateSizeStar; i++)
            {
                _upgradeSize.ChangeSize();
                AddLineUpgrade();
            }
        }
    }

    public override void MaxUpgardeButton()
    {
        if (!CanUpgradeCount())
        {
            _viewModelUpgrade.CostSizeStar = "max";
        }
    }
}
