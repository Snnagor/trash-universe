using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableButtonsBuy : MonoBehaviour
{
    [SerializeField] private PlatformBuy recyclingButtonBuy;
    [SerializeField] private PlatformBuy autotakerButtonBuy;
    [SerializeField] private PlatformBuy nextLevelButtonBuy;

    
    public void RecyclingButtonBuyShow()
    {
       recyclingButtonBuy.ShowBuyButton();
    }

    public void AutotakerButtonBuyShow()
    {
        autotakerButtonBuy.ShowBuyButton();
    }

    public void NextLevelButtonBuyShow()
    {
       nextLevelButtonBuy.ShowBuyButton();
    }

    public void EnableButtons()
    {
        recyclingButtonBuy.EnableButtonBuy();
        autotakerButtonBuy.EnableButtonBuy();   
    }
    
    public void DisableButtons()
    {
        recyclingButtonBuy.DisableButtonBuy();
        autotakerButtonBuy.DisableButtonBuy();    
    }

    public void EnableNextLevelButton()
    {
        nextLevelButtonBuy.EnableButtonBuy();
    }

    public void DisableNextLevelButton()
    {
        nextLevelButtonBuy.DisableButtonBuy();
    }

}
