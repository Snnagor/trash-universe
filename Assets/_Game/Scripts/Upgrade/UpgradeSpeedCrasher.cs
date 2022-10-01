using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpeedCrasher : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float startSpeed;
    [SerializeField] private float deltaSpeed;

    private void Awake()
    {
        anim.speed = startSpeed;
    }

    public void ChangeSpeed()
    {
        anim.speed += deltaSpeed;
    }
}
