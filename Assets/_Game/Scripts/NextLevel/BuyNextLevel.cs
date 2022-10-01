using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuyNextLevel : PlatformBuy
{
    #region Injects

    private BusMove _busMove;

    [Inject]
    private void Construct(BusMove busMove)
    {
        _busMove = busMove;
    }

    #endregion

    public override void Start()
    {
        if (_busMove.BusArrival)
        {
            gameObject.SetActive(false);
        }
    }

    public override void BuyPlatform()
    {
        int needScore = NeedScore();

        if (needScore < 0) return;

        if (ScoreManager.TotalMoney >= needScore)
        {
            PlatformManager.CurrentNextLevelMoney += needScore;
            ScoreManager.TmpMoney -= needScore;

            ShowPlatform();            
        }
        else
        {
            PlatformManager.CurrentNextLevelMoney += Mathf.RoundToInt(ScoreManager.TmpMoney);
            ScoreManager.TmpMoney -= ScoreManager.TmpMoney;
        }
    }

    public override void ShowPlatform()
    {
        gameObject.SetActive(false);
        _busMove.ArrivalBus();
    }

    public override int NeedScore()
    {
        return PlatformManager.CostNextLevel - PlatformManager.CurrentNextLevelMoney;
    }
}
