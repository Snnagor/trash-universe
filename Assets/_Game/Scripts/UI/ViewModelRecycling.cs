using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class ViewModelRecycling : MonoBehaviour, INotifyPropertyChanged
{    
    private string paper = "0";
    private string cardboard = "0";
    private string tire = "0";
    private string bottle = "0";

    private string costPaperToToiletPaper = "0";
    private string costCardboardToPizzaBox = "0";
    private string costTireToDuck = "0";
    private string costBottleToGlasses = "0";

    private string toiletPaper = "0";
    private string pizzaBox = "0";
    private string littleDuck = "0";
    private string glasses = "0";

    public event PropertyChangedEventHandler PropertyChanged;

    [Binding]
    public string Paper 
    { 
        get => paper;
        set 
        {
            if (paper.Equals(value)) return;

            paper = value;
            OnPropertyChanged("Paper");
        }
    }

    [Binding]
    public string Cardboard
    {
        get => cardboard;
        set
        {
            if (cardboard.Equals(value)) return;

            cardboard = value;
            OnPropertyChanged("Cardboard");
        }
    }
        
    [Binding]
    public string ToiletPaper
    {
        get => toiletPaper;
        set
        {
            if (toiletPaper.Equals(value)) return;

            toiletPaper = value;
            OnPropertyChanged("ToiletPaper");
        }
    }   

    [Binding]
    public string PizzaBox
    {
        get => pizzaBox;
        set
        {
            if (pizzaBox.Equals(value)) return;

            pizzaBox = value;
            OnPropertyChanged("PizzaBox");
        }
    }

    [Binding]
    public string Tire
    {
        get => tire;
        set
        {
            if (tire.Equals(value)) return;

            tire = value;
            OnPropertyChanged("Tire");
        }
    }        

    [Binding]
    public string LittleDuck
    {
        get => littleDuck;
        set
        {
            if (littleDuck.Equals(value)) return;

            littleDuck = value;
            OnPropertyChanged("LittleDuck");
        }
    }        

    [Binding]
    public string Bottle
    {
        get => bottle;
        set
        {
            if (bottle.Equals(value)) return;

            bottle = value;
            OnPropertyChanged("Bottle");
        }
    }

    [Binding]
    public string Glasses
    {
        get => glasses;
        set
        {
            if (glasses.Equals(value)) return;

            glasses = value;
            OnPropertyChanged("Glasses");
        }
    }

    [Binding]
    public string CostPaperToToiletPaper
    {
        get => costPaperToToiletPaper;
        set
        {
            if (costPaperToToiletPaper.Equals(value)) return;

            costPaperToToiletPaper = value + "/1";
            OnPropertyChanged("CostPaperToToiletPaper");
        }
    }

    [Binding]
    public string CostCardboardToPizzaBox
    {
        get => costCardboardToPizzaBox;
        set
        {
            if (costCardboardToPizzaBox.Equals(value)) return;

            costCardboardToPizzaBox = value + "/1";
            OnPropertyChanged("CostCardboardToPizzaBox");
        }
    }

    [Binding]
    public string CostTireToDuck
    {
        get => costTireToDuck;
        set
        {
            if (costTireToDuck.Equals(value)) return;

            costTireToDuck = value + "/1";
            OnPropertyChanged("CostTireToDuck");
        }
    }

    [Binding]
    public string CostBottleToGlasses
    {
        get => costBottleToGlasses;
        set
        {
            if (costBottleToGlasses.Equals(value)) return;

            costBottleToGlasses = value + "/1";
            OnPropertyChanged("CostBottleToGlasses");
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
