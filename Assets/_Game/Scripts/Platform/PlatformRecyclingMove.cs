using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRecyclingMove : PlatformMoveHorizontal
{
    [SerializeField] private GameObject uiPanel;

    public override void Start()
    {
        base.Start();

        if (Built)
        {
            uiPanel.SetActive(true);
        }
    }

    public override void CallBackArrival()
    {
        uiPanel.SetActive(true);
        base.CallBackArrival();
    }
}
