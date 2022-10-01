using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBuyAutotaker : PlatformBuy
{
    public override void BuyPlatform()
    {
        int needScore = NeedScore();

        if (needScore <= 0) return;

        if (ScoreManager.TotalMoney >= needScore)
        {            
            PlatformManager.CurrentAutotakerPlatformMoney += needScore;
            ScoreManager.TmpMoney -= needScore;

            ShowPlatform();            
        }
        else
        {
            PlatformManager.CurrentAutotakerPlatformMoney += Mathf.RoundToInt(ScoreManager.TmpMoney);
            ScoreManager.TmpMoney -= ScoreManager.TmpMoney;
        }
    }

    public override int NeedScore()
    {
        return PlatformManager.CostAutoTakerPlatform - PlatformManager.CurrentAutotakerPlatformMoney;
    }
}
