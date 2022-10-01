using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeTrailerSpace : UIUpgradeContoller
{
    public override void BtnUpgrade()
    {
        if (!CanUpgradeCount()) return;

        if (!CanUpgradeCost())
        {
            _uIController.ShopPanel();
            return;
        }

        _upgradeTrailer.ChangeSpaceTrailer();
        base.BtnUpgrade();

        _serialDataManager.Data.IdUpdateTrailer = currentLineUpgrade;
    }

    public override int GetCostUpgrade(int idUpgrade)
    {
        if (idUpgrade == _settings.CostSpaceTraile.Length) return 0;
        return _settings.CostSpaceTraile[idUpgrade];
    }

    public override void UpdatePriceToButton(int price)
    {
        _viewModelUpgrade.CostSpaceTrailer = price.ToString() + "$";
    }

    public override void LoadUpgradeData()
    {
        int idUpdateTrailer = _serialDataManager.Data.IdUpdateTrailer;

        if (idUpdateTrailer > 0)
        {
            for (int i = 0; i < idUpdateTrailer; i++)
            {
                _upgradeTrailer.ChangeSpaceTrailer();
                AddLineUpgrade();
            }

        }
    }

    public override void MaxUpgardeButton()
    {
        if (!CanUpgradeCount())
        {
            _viewModelUpgrade.CostSpaceTrailer = "max";
        }
    }
}
