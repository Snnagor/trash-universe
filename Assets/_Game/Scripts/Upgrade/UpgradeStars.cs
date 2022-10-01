using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStars : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    public GameObject[] Stars { get => stars; set => stars = value; }
}
