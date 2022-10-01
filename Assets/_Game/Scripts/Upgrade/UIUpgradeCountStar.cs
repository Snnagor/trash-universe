using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeCountStar : UIUpgradeContoller
{   
    public override void BtnUpgrade()
    {        
        if (!CanUpgradeCount()) return;

        if (!CanUpgradeCost())
        {
            _uIController.ShopPanel();
            return;
        }

        _upgradeLadle.AddStar();
        _upgradeForceCrusher.ChangeForceCrusher();
        base.BtnUpgrade();
        _serialDataManager.Data.IdUpdateCountStar = currentLineUpgrade;
    }

    public override int GetCostUpgrade(int idUpgrade)
    {
        if (idUpgrade == _settings.CostCountStar.Length) return 0;

        return _settings.CostCountStar[idUpgrade];
    }

    public override void LoadUpgradeData()
    {        
        int idUpdateCountStar = _serialDataManager.Data.IdUpdateCountStar;

        if (idUpdateCountStar > 0)
        {
            for (int i = 0; i < idUpdateCountStar; i++)
            {
                _upgradeLadle.EnableOneStar();
                _upgradeForceCrusher.ChangeForceCrusher();
                AddLineUpgrade();
            }

            _upgradeLadle.SetPositionStar();

        }
    }

    public override void MaxUpgardeButton()
    {
        if (!CanUpgradeCount())
        {
            _viewModelUpgrade.CostCountStar = "max";
        }
    }

    public override void UpdatePriceToButton(int price)
    {
        _viewModelUpgrade.CostCountStar = price.ToString() + "$";
    }
}
