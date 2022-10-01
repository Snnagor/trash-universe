using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBonusManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] statesBonus;
    private List<IStateForBonus> statesIStateForBonus = new List<IStateForBonus>();

    private void Start()
    {
        foreach (var item in statesBonus)
        {
            if(item is IStateForBonus)
            {
                statesIStateForBonus.Add(item as IStateForBonus);
            }
            else
            {
                Debug.LogError("This is not IStateForBonus: " + item.name);
            }
        }
    }

    public Bonus GetBestBonus()
    {
        float bestEvaluate = 0;
        List<Bonus> bestBonus = new List<Bonus>();
        int index = 0;

        foreach (var item in statesIStateForBonus)
        {
            if(bestEvaluate <= item.Evaluate )
            {
                if(item.Evaluate > bestEvaluate)
                {
                    bestBonus.Clear();   
                }

                bestEvaluate = item.Evaluate;
                bestBonus.Add(item.GetBonus);
            }
        }

        if(bestBonus.Count > 1)
        {
           index  = Random.Range(0, bestBonus.Count);
        }       

        return bestBonus[index];
    }       
}
