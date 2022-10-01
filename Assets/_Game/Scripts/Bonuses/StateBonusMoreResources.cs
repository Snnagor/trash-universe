using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateBonusMoreResources : MonoBehaviour, IStateForBonus
{
    [SerializeField] private string nameState;
    public string NameState { get => nameState; set => nameState = value; }

    [SerializeField] private StateBonusFullTrailer stateBonusFullTrailer;

    [SerializeField] private Bonus[] bonuses;

    [SerializeField] private int maxCountResources;

    public float Evaluate { get => GetCountResources() / 10; }

    public Bonus GetBonus => bonuses[0];

    #region Injects

    private PlatformMainTrashControl _trashControl;
    private TrailerTrash _trailerTrash;

    [Inject]
    private void Construct(PlatformMainTrashControl trashControl,
                           TrailerTrash trailerTrash)
    {
        _trashControl = trashControl;
        _trailerTrash = trailerTrash;
    }

    #endregion 

    private int GetCountResources()
    {        
        int percentResources = _trashControl.CreatedPackedTrash * 100 / maxCountResources;
        int percentFullTrailer = _trailerTrash.ItemInTrailer.Count * 100 / _trailerTrash.CountMaxItemInTrailer;

        if (percentFullTrailer > 90 && percentResources > percentFullTrailer)
        {
            int delta = percentResources - percentFullTrailer;
            percentResources -= (delta + 1);
        }

        return percentResources;
    }    
}
