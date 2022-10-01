using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

[Binding]
public class ViewModelShop : MonoBehaviour, INotifyPropertyChanged
{
    private string junkBotSetDeltaSpeed = "0";   
    private string junkBotSetCountHard = "0";   
    private string junkBotSetCost = "0";  
    
    private string everyDayCountHard = "0";   

    private string specialOfferCountCoins = "0";   
    private string specialOfferCountHard = "0";   
    private string specialOfferCost = "0";      

    private string smallCoins = "0";   
    private string smallCoinsCost = "0";

    private string middleCoins = "0";
    private string middleCoinsCost = "0";

    private string bigCoins = "0";
    private string bigCoinsCost = "0";

    private string smallHard = "0";
    private string smallHardCost = "0";

    private string middleHard = "0";
    private string middleHardCost = "0";

    private string bigHard = "0";
    private string bigHardCost = "0";

    private string boostCrusherCost = "0";
    private string boostForceCost = "0";
    private string boostTrailerCost = "0";
    private string boostMagnetCost = "0";
    private string noAdsCost = "0";
        

    public event PropertyChangedEventHandler PropertyChanged;

    #region Injects

    private SettingsShop _settingsShop;

    [Inject]
    private void Construct(SettingsShop settingsShop)
    {
        _settingsShop = settingsShop;
    }

    #endregion

    private void Start()
    {
        JunkBotSetDeltaSpeed = $"Mega cool HEADPHONES +{_settingsShop.JunkBotPercentDeltaSpeed}% SPEED";
        JunkBotSetCountHard = $"+{_settingsShop.JunkBotCountCrystals} CRYSTALS";
        JunkBotSetCost = $"{_settingsShop.JunkBotCostMoney}$";

        EveryDayCountHard = _settingsShop.EveryDayCrystals.ToString();

        SpecialOfferCountCoins = _settingsShop.SpecialOfferCoins.ToString();
        SpecialOfferCountHard = _settingsShop.SpecialOfferCristals.ToString();
        SpecialOfferCost = $"{_settingsShop.SpecialOfferCostMoney}$";

        SmallCoins = _settingsShop.SmallCoins.ToString();
        SmallCoinsCost = _settingsShop.SmallCoinsCost.ToString();
        MiddleCoins = _settingsShop.MiddleCoins.ToString();
        MiddleCoinsCost = _settingsShop.MiddleCoinsCost.ToString();
        BigCoins = _settingsShop.BigCoins.ToString();
        BigCoinsCost = _settingsShop.BigCoinsCost.ToString();


        SmallHard = _settingsShop.SmallCrystals.ToString();
        SmallHardCost = $"{_settingsShop.SmallCrystalsCostMoney}$";

        MiddleHard = _settingsShop.MiddleCrystals.ToString();
        MiddleHardCost = $"{_settingsShop.MiddleCrystalsCostMoney}$";

        BigHard = _settingsShop.BigCrystals.ToString();
        BigHardCost = $"{_settingsShop.BigCrystalsCostMoney}$";

        BoostCrusherCost = $"{_settingsShop.BoostCrusherCostMoney}$";
        BoostForceCost = $"{_settingsShop.BoostForceCostMoney}$";
        BoostTrailerCost = $"{_settingsShop.BoostTrailerCostMoney}$";
        BoostMagnetCost = $"{_settingsShop.BoostMagnetCostMoney}$";
        NoAdsCost = $"{_settingsShop.TrueNoAdsCostMoney}$";
    }

    [Binding]
    public string JunkBotSetDeltaSpeed
    { 
        get => junkBotSetDeltaSpeed;
        set 
        {
            if (junkBotSetDeltaSpeed.Equals(value)) return;

            junkBotSetDeltaSpeed = value;
            OnPropertyChanged("JunkBotSetDeltaSpeed");
        }
    }

    [Binding]
    public string JunkBotSetCountHard
    {
        get => junkBotSetCountHard;
        set
        {
            if (junkBotSetCountHard.Equals(value)) return;

            junkBotSetCountHard = value;
            OnPropertyChanged("JunkBotSetCountHard");
        }
    }

    [Binding]
    public string JunkBotSetCost
    {
        get => junkBotSetCost;
        set
        {
            if (junkBotSetCost.Equals(value)) return;

            junkBotSetCost = value;
            OnPropertyChanged("JunkBotSetCost");
        }
    }

    [Binding]
    public string EveryDayCountHard
    {
        get => everyDayCountHard;
        set
        {
            if (everyDayCountHard.Equals(value)) return;

            everyDayCountHard = value;
            OnPropertyChanged("EveryDayCountHard");
        }
    }

    [Binding]
    public string SpecialOfferCountCoins
    {
        get => specialOfferCountCoins;
        set
        {
            if (specialOfferCountCoins.Equals(value)) return;

            specialOfferCountCoins = value;
            OnPropertyChanged("SpecialOfferCountCoins");
        }
    }

    [Binding]
    public string SpecialOfferCountHard
    {
        get => specialOfferCountHard;
        set
        {
            if (specialOfferCountHard.Equals(value)) return;

            specialOfferCountHard = value;
            OnPropertyChanged("SpecialOfferCountHard");
        }
    }

    [Binding]
    public string SpecialOfferCost
    {
        get => specialOfferCost;
        set
        {
            if (specialOfferCost.Equals(value)) return;

            specialOfferCost = value;
            OnPropertyChanged("SpecialOfferCost");
        }
    }

    [Binding]
    public string SmallCoins
    {
        get => smallCoins;
        set
        {
            if (smallCoins.Equals(value)) return;

            smallCoins = value;
            OnPropertyChanged("SmallCoins");
        }
    }

    [Binding]
    public string SmallCoinsCost
    {
        get => smallCoinsCost;
        set
        {
            if (smallCoinsCost.Equals(value)) return;

            smallCoinsCost = value;
            OnPropertyChanged("SmallCoinsCost");
        }
    }

    [Binding]
    public string MiddleCoins
    {
        get => middleCoins;
        set
        {
            if (middleCoins.Equals(value)) return;

            middleCoins = value;
            OnPropertyChanged("MiddleCoins");
        }
    }

    [Binding]
    public string MiddleCoinsCost
    {
        get => middleCoinsCost;
        set
        {
            if (middleCoinsCost.Equals(value)) return;

            middleCoinsCost = value;
            OnPropertyChanged("MiddleCoinsCost");
        }
    }

    [Binding]
    public string BigCoins
    {
        get => bigCoins;
        set
        {
            if (bigCoins.Equals(value)) return;

            bigCoins = value;
            OnPropertyChanged("BigCoins");
        }
    }

    [Binding]
    public string BigCoinsCost
    {
        get => bigCoinsCost;
        set
        {
            if (bigCoinsCost.Equals(value)) return;

            bigCoinsCost = value;
            OnPropertyChanged("BigCoinsCost");
        }
    }

    [Binding]
    public string SmallHard
    {
        get => smallHard;
        set
        {
            if (smallHard.Equals(value)) return;

            smallHard = value;
            OnPropertyChanged("SmallHard");
        }
    }

    [Binding]
    public string SmallHardCost
    {
        get => smallHardCost;
        set
        {
            if (smallHardCost.Equals(value)) return;

            smallHardCost = value;
            OnPropertyChanged("SmallHardCost");
        }
    }

    [Binding]
    public string MiddleHard
    {
        get => middleHard;
        set
        {
            if (middleHard.Equals(value)) return;

            middleHard = value;
            OnPropertyChanged("MiddleHard");
        }
    }

    [Binding]
    public string MiddleHardCost
    {
        get => middleHardCost;
        set
        {
            if (middleHardCost.Equals(value)) return;

            middleHardCost = value;
            OnPropertyChanged("MiddleHardCost");
        }
    }

    [Binding]
    public string BigHard
    {
        get => bigHard;
        set
        {
            if (bigHard.Equals(value)) return;

            bigHard = value;
            OnPropertyChanged("BigHard");
        }
    }

    [Binding]
    public string BigHardCost
    {
        get => bigHardCost;
        set
        {
            if (bigHardCost.Equals(value)) return;

            bigHardCost = value;
            OnPropertyChanged("BigHardCost");
        }
    }

    [Binding]
    public string BoostCrusherCost
    {
        get => boostCrusherCost;
        set
        {
            if (boostCrusherCost.Equals(value)) return;

            boostCrusherCost = value;
            OnPropertyChanged("BoostCrusherCost");
        }
    }

    [Binding]
    public string BoostForceCost
    {
        get => boostForceCost;
        set
        {
            if (boostForceCost.Equals(value)) return;

            boostForceCost = value;
            OnPropertyChanged("BoostForceCost");
        }
    }

    [Binding]
    public string BoostTrailerCost
    {
        get => boostTrailerCost;
        set
        {
            if (boostTrailerCost.Equals(value)) return;

            boostTrailerCost = value;
            OnPropertyChanged("BoostTrailerCost");
        }
    }

    [Binding]
    public string BoostMagnetCost
    {
        get => boostMagnetCost;
        set
        {
            if (boostMagnetCost.Equals(value)) return;

            boostMagnetCost = value;
            OnPropertyChanged("BoostMagnetCost");
        }
    }

    [Binding]
    public string NoAdsCost
    {
        get => noAdsCost;
        set
        {
            if (noAdsCost.Equals(value)) return;

            noAdsCost = value;
            OnPropertyChanged("NoAdsCost");
        }
    }


    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
