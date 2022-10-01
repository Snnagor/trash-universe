using UnityEngine;

public interface IStateForBonus
{
    public string NameState { get; set; }

    public float Evaluate { get; }

    public Bonus GetBonus { get; }

}
