using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeForceCrusher : MonoBehaviour
{
    [SerializeField] private Front front;
    [SerializeField] private float deltaForce = 0.1f;

    public void ChangeForceCrusher()
    {
        front.ForceCrusher += deltaForce;
    }
}
