using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

[Binding]
public class ViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private Color colorFillImage = Color.green;
    private Single fillImage = 0;
    private string totalMoney = "0";
    private string totalHard = "0";
    private string itemInTrailer = "0";
    private Single trashInPlatform = 1;
    private string fps = " ";


    private string countCoinsOneTimeOffer = "0";
    private string countHardOneTimeOffer = "0";
    private string oldCostOneTimeOffer = "0";
    private string costOneTimeOffer = "0";

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
        CountCoinsOneTimeOffer = $"{_settingsShop.CountCoinOneTimeOffer}";
        CountHardOneTimeOffer = $"{_settingsShop.CountCrystalsOneTimeOffer}";
        OldCostOneTimeOffer = $"{_settingsShop.OldCostOneTimeOffer}$";
        CostOneTimeOffer = $"{_settingsShop.CostOneTimeOffer}$";
    }

    [Binding]
    public string TotalMoney
    {
        get => totalMoney;
        set
        {
            if (totalMoney.Equals(value)) return;

            totalMoney = value;
            OnPropertyChanged("TotalMoney");
        }
    }

    [Binding]
    public string TotalHard
    {
        get => totalHard;
        set
        {
            if (totalHard.Equals(value)) return;

            totalHard = value;
            OnPropertyChanged("TotalHard");
        }
    }

    [Binding]
    public string ItemInTrailer
    {
        get => itemInTrailer;
        set
        {
            if (itemInTrailer.Equals(value)) return;

            itemInTrailer = value;
            OnPropertyChanged("ItemInTrailer");
        }
    }

    [Binding]
    public string Fps
    {
        get => fps;
        set
        {
            if (fps.Equals(value)) return;

            fps = value;
            OnPropertyChanged("Fps");
        }
    }

    [Binding]
    public Single FillImage
    {
        get => fillImage;
        set
        {
            if (fillImage.Equals(value)) return;

            fillImage = value;
            OnPropertyChanged("FillImage");
        }
    }

    [Binding]
    public Single TrashInPlatform
    {
        get => trashInPlatform;
        set
        {
            if (trashInPlatform.Equals(value)) return;

            trashInPlatform = value;
            OnPropertyChanged("TrashInPlatform");
        }
    }

    [Binding]
    public Color ColorFillImage
    {
        get => colorFillImage;
        set
        {
            if (colorFillImage.Equals(value)) return;

            colorFillImage = value;
            OnPropertyChanged("ColorFillImage");
        }
    }

    [Binding]
    public string CountCoinsOneTimeOffer
    {
        get => countCoinsOneTimeOffer;
        set
        {
            if (countCoinsOneTimeOffer.Equals(value)) return;

            countCoinsOneTimeOffer = value;
            OnPropertyChanged("CountCoinsOneTimeOffer");
        }
    }

    [Binding]
    public string CountHardOneTimeOffer
    {
        get => countHardOneTimeOffer;
        set
        {
            if (countHardOneTimeOffer.Equals(value)) return;

            countHardOneTimeOffer = value;
            OnPropertyChanged("CountHardOneTimeOffer");
        }
    }

    [Binding]
    public string OldCostOneTimeOffer
    {
        get => oldCostOneTimeOffer;
        set
        {
            if (oldCostOneTimeOffer.Equals(value)) return;

            oldCostOneTimeOffer = value;
            OnPropertyChanged("OldCostOneTimeOffer");
        }
    }

    [Binding]
    public string CostOneTimeOffer
    {
        get => costOneTimeOffer;
        set
        {
            if (costOneTimeOffer.Equals(value)) return;

            costOneTimeOffer = value;
            OnPropertyChanged("CostOneTimeOffer");
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
