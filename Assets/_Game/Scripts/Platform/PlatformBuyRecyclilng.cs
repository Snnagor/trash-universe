using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBuyRecyclilng : PlatformBuy
{   
    public override void BuyPlatform()
    {
        int needScore = NeedScore();

        if (needScore < 0) return;

        if (ScoreManager.TotalMoney >= needScore)
        {
            PlatformManager.CurrentRecyclingPlatformMoney += needScore;
            ScoreManager.TmpMoney -= needScore;

            ShowPlatform();            
        }
        else
        {
            PlatformManager.CurrentRecyclingPlatformMoney += Mathf.RoundToInt(ScoreManager.TmpMoney);
            ScoreManager.TmpMoney -= ScoreManager.TmpMoney;
        }
    }

    public override int NeedScore()
    {
        return PlatformManager.CostRecyclingPlatform - PlatformManager.CurrentRecyclingPlatformMoney;
    }
}
