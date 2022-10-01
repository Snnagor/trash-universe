using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCameraBrain : MonoBehaviour
{
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private float deltaTime;

    public void Init()
    {
        brain.m_DefaultBlend.m_Time = 1f; 
    }

    public void ChangeTimeMoveCamera()
    {
        brain.m_DefaultBlend.m_Time -= deltaTime;
    }

}
