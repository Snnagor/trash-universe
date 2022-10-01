using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateBonusFullTrailer : MonoBehaviour, IStateForBonus
{
    [SerializeField] private string nameState;
    public string NameState { get => nameState; set => nameState = value; }

    [SerializeField] private Bonus[] bonuses;

    public float Evaluate { get => GetPercentFulTrailer() / 10; }

    public Bonus GetBonus => bonuses[0];

    #region Injects

    private TrailerTrash _trailerTrash;

    [Inject]
    private void Construct(TrailerTrash trailerTrash)
    {
        _trailerTrash = trailerTrash;
    }

    #endregion 

    private int GetPercentFulTrailer()
    {
       return _trailerTrash.ItemInTrailer.Count * 100 / _trailerTrash.CountMaxItemInTrailer;
    }
  
}
