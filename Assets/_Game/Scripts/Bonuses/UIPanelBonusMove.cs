using UnityEngine;

public class UIPanelBonusMove : UIPanelMove
{
    [SerializeField] private GameObject freeBonusPanel;
    [SerializeField] private GameObject payBonusPanel;
    [SerializeField] private BonusesManager _bonusManager;


    private void Start()
    {
        if (_bonusManager.FirstBonus)
        {
            payBonusPanel.SetActive(true);
            freeBonusPanel.SetActive(false);
        }
        else
        {
            freeBonusPanel.SetActive(true);
            payBonusPanel.SetActive(false);
        }
    }

    public void WatchAds()
    {
        HidePanel();
        _adsManager.ShowRewardedAd();        
    }
        

    public void BuyHard()
    {     
        if (_scoreManager.TotalHard < _bonusManager.CurrentBestBonus.CostBonus) 
        {
            _btnController.ShopBtn();
            return;
        }


        HidePanel();
        _bonusManager.InitActiveBonus();
        _scoreManager.TotalHard -= _bonusManager.CurrentBestBonus.CostBonus;
    }

    public void FreeBonus()
    {
        HidePanel();
        _bonusManager.InitActiveBonus();
        _bonusManager.FirstBonus = true;        
    }

    public override void CallBackHide()
    {
        payBonusPanel.SetActive(true);
        freeBonusPanel.SetActive(false);
    }
    public override void CloseBtn()
    {
        base.CloseBtn();

        if (_bonusManager.PauseBonus)
        {
            _bonusManager.PauseBonus = false;
        }
    }
}
