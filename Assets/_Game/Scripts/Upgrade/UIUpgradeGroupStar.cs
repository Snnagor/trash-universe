using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeGroupStar : UIUpgradeContoller
{
    public override void BtnUpgrade()
    {
        if (!CanUpgradeCount()) return;       

        if (!CanUpgradeCost())
        {
            _uIController.ShopPanel();
            return;
        }

        _upgradeLadle.ChangeGroupStar();
        _upgradeForceCrusher.ChangeForceCrusher();        

        base.BtnUpgrade();

        _serialDataManager.Data.IdUpdateGroupStar = currentLineUpgrade;
    }

    public override int GetCostUpgrade(int idUpgrade)
    {       
        if (idUpgrade == _settings.CostGroupStar.Length) return 0;
        return _settings.CostGroupStar[idUpgrade];
    }

    public override void UpdatePriceToButton(int price)
    {
        _viewModelUpgrade.CostGroupStar = price.ToString() + "$";
    }

    public override void LoadUpgradeData()
    {
        int idUpdateGroupStar = _serialDataManager.Data.IdUpdateGroupStar;

        if(idUpdateGroupStar > 0)
        {
            for (int i = 0; i < idUpdateGroupStar; i++)
            {
                _upgradeLadle.ChangeGroupStar();
                _upgradeForceCrusher.ChangeForceCrusher();
                AddLineUpgrade();
            }
                       
        }       
    }

    public override void MaxUpgardeButton()
    {
        if (!CanUpgradeCount())
        {
            _viewModelUpgrade.CostGroupStar = "max";
        }
    }
}
