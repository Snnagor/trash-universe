using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreManager : MonoBehaviour
{
    private int totalHard;
    public int TotalHard
    {
        get
        {
            return totalHard;
        }
        set
        {
            totalHard = value;
            _viewModel.TotalHard = totalHard.ToString();
        }
    }

    private int totalMoney;
    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;            
            _viewModel.TotalMoney = totalMoney.ToString();
        }
    }

    private float tmpMoney;
    public float TmpMoney 
    { 
        get
        {
            return tmpMoney;
        }
        set 
        {
            tmpMoney = value;
            TotalMoney = Mathf.RoundToInt(tmpMoney);

            if (!firstSell && tmpMoney > 0)
            {
                _signalBus.Fire(new FirstSellSignal());
                firstSell = true;
            }
        }            
    }

    private int paper;

    public int Paper 
    {
        get
        {
            return paper;
        }
        set
        {
            paper = value;
            _viewModelRecycling.Paper = paper.ToString();
        }
    }

    private int tire;

    public int Tire
    {
        get
        {
            return tire;
        }
        set
        {
            tire = value;
            _viewModelRecycling.Tire = tire.ToString();
        }
    }

    private int bottle;

    public int Bottle
    {
        get
        {
            return bottle;
        }
        set
        {
            bottle = value;
            _viewModelRecycling.Bottle = bottle.ToString();
        }
    }

    private bool firstSell = false;

    #region Injects

    private SignalBus _signalBus;
    private ViewModel _viewModel;
    private ViewModelRecycling _viewModelRecycling;
    private SoundManager _soundManager;
   // private RecyclingData _recyclingData;

    [Inject]
    private void Construct(SignalBus signalBus, 
                           ViewModel viewModel, 
                           ViewModelRecycling viewModelRecycling,
                           SoundManager soundManager
                           /*RecyclingData recyclingData*/)
    {
        _signalBus = signalBus;
        _viewModel = viewModel;
        _viewModelRecycling = viewModelRecycling;
        _soundManager = soundManager;
       // _recyclingData = recyclingData;
    }

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
 }
