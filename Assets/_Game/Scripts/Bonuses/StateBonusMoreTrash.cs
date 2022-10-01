using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateBonusMoreTrash : MonoBehaviour, IStateForBonus
{
    [SerializeField] private string nameState;
    public string NameState { get => nameState; set => nameState = value; }

    [SerializeField] private Bonus[] bonuses;

    public float Evaluate { get => GetPercentTrash() / 10; }

    public Bonus GetBonus => bonuses[0];

    int currentCountTrashPieces;

    #region Injects

    private PlatformMainTrashControl _trashControl;

    [Inject]
    private void Construct(PlatformMainTrashControl trashControl)
    {
        _trashControl = trashControl;
    }

    #endregion 

    private int GetPercentTrash()
    {
        int countTotalMainTrash = _trashControl.GetCountAllMainTrash();        

        int countTotalTrashPieces = _trashControl.GetTotalPieceOneTrash() * countTotalMainTrash;

        currentCountTrashPieces = 0;

        foreach (var item in _trashControl.AllMainTrash)
        {
            currentCountTrashPieces += item.GetCurrentCountPeaces();
        }

        return currentCountTrashPieces * 100 / countTotalTrashPieces;
    }
}
