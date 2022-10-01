using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class ViewModelBase : MonoBehaviour, INotifyPropertyChanged
{    
    private string costRecyclig = "0";
    private string costAutotaker = "0";
    private string costNextLevel = "0";

    private string currentCostRecycling = "0";
    private string currentCostAutotaker = "0";
    private string currentCostNextLevel = "0";
    
    public event PropertyChangedEventHandler PropertyChanged;

    [Binding]
    public string CostRecyclig
    {
        get => costRecyclig;
        set
        {
            if (costRecyclig.Equals(value)) return;

            costRecyclig = value + "$";
            OnPropertyChanged("CostRecyclig");
        }
    }

    [Binding]
    public string CostAutotaker
    {
        get => costAutotaker;
        set
        {
            if (costAutotaker.Equals(value)) return;

            costAutotaker = value + "$";
            OnPropertyChanged("CostAutotaker");
        }
    }

    [Binding]
    public string CostNextLevel
    {
        get => costNextLevel;
        set
        {
            if (costNextLevel.Equals(value)) return;

            costNextLevel = value + "$";
            OnPropertyChanged("CostNextLevel");
        }
    }

    [Binding]
    public string CurrentCostRecycling
    {
        get => currentCostRecycling;
        set
        {
            if (currentCostRecycling.Equals(value)) return;

            currentCostRecycling = value;
            OnPropertyChanged("CurrentCostRecycling");
        }
    }

    [Binding]
    public string CurrentCostAutotaker
    {
        get => currentCostAutotaker;
        set
        {
            if (currentCostAutotaker.Equals(value)) return;

            currentCostAutotaker = value;
            OnPropertyChanged("CurrentCostAutotaker");
        }
    }

    [Binding]
    public string CurrentCostNextLevel
    {
        get => currentCostNextLevel;
        set
        {
            if (currentCostNextLevel.Equals(value)) return;

            currentCostNextLevel = value;
            OnPropertyChanged("CurrentCostNextLevel");
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
