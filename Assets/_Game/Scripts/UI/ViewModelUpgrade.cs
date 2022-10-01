using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class ViewModelUpgrade : MonoBehaviour, INotifyPropertyChanged
{    
    private string costCountStar = "free";
    private string costGroupStar = "free";
    private string costSizeStar = "free";
    private string costSpeedStar = "free";
    private string costSpaceTrailer = "free";
    private string costSpeedWheels = "free";
    

    public event PropertyChangedEventHandler PropertyChanged;

    [Binding]
    public string CostCountStar
    { 
        get => costCountStar;
        set 
        {
            if (costCountStar.Equals(value)) return;
                       
            string tmp = value;

            if (value == "0$")
            {
                tmp = "free";
            }

            costCountStar = tmp;
            OnPropertyChanged("CostCountStar");
        }
    }

    [Binding]
    public string CostGroupStar
    {
        get => costGroupStar;
        set
        {
            if (costGroupStar.Equals(value)) return;

            string tmp = value;

            if (value == "0$")
            {
                tmp = "free";
            }

            costGroupStar = tmp;
            OnPropertyChanged("CostGroupStar");
        }
    }

    [Binding]
    public string CostSizeStar
    {
        get => costSizeStar;
        set
        {
            if (costSizeStar.Equals(value)) return;

            string tmp = value;

            if (value == "0$")
            {
                tmp = "free";
            }

            costSizeStar = tmp;
            OnPropertyChanged("CostSizeStar");
        }
    }

    [Binding]
    public string CostSpeedStar
    {
        get => costSpeedStar;
        set
        {
            if (costSpeedStar.Equals(value)) return;

            string tmp = value;

            if (value == "0$")
            {
                tmp = "free";
            }

            costSpeedStar = tmp;
            OnPropertyChanged("CostSpeedStar");
        }
    }

    [Binding]
    public string CostSpaceTrailer
    {
        get => costSpaceTrailer;
        set
        {
            if (costSpaceTrailer.Equals(value)) return;

            string tmp = value;

            if (value == "0$")
            {
                tmp = "free";
            }

            costSpaceTrailer = tmp;
            OnPropertyChanged("CostSpaceTrailer");
        }
    }

    [Binding]
    public string CostSpeedWheels
    {
        get => costSpeedWheels;
        set
        {
            if (costSpeedWheels.Equals(value)) return;

            string tmp = value;

            if (value == "0$")
            {
                tmp = "free";
            }

            costSpeedWheels = tmp;
            OnPropertyChanged("CostSpeedWheels");
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
