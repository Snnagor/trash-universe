using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateBonusForceTrash : MonoBehaviour, IStateForBonus
{
    [SerializeField] private string nameState;
    public string NameState { get => nameState; set => nameState = value; }

    [SerializeField] private Bonus[] bonuses;

    [SerializeField] private float maxDelatResistValue = 6;

    public float Evaluate { get => GetResist() / 10; }

    public Bonus GetBonus => bonuses[0];

    #region Injects

    private PlatformMainTrashControl _trashControl;
    private Front _front;

    [Inject]
    private void Construct(PlatformMainTrashControl trashControl,
                           Front front)
    {
        _trashControl = trashControl;
        _front = front;
    }

    #endregion 

    private int GetResist()
    {

        float currentTrashResist =_trashControl.AllMainTrash[0].GetResist();
        float curretnForceCrusher = _front.ForceCrusher;

        float deltaResist = currentTrashResist - curretnForceCrusher;

        return Mathf.RoundToInt(deltaResist * 100 / maxDelatResistValue); 
    }

}
