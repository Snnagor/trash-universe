using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyTrash : MonoBehaviour
{
    public float Resist { get; set; }
    public Vector3 PositionFront { get; set; }

    MainTrash mainTrash;

    private int health;
    private int damage;
    
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                DestroyThisTrash();
            }
        }
    }

    public void SetSettings(int healthValue, float resistValue)
    {
        health = healthValue;       
        Resist = resistValue;        
    }

    private void Awake()
    {
        mainTrash = GetComponentInParent<MainTrash>();       
    }

    public void DamagePeace(int damageValue)
    {       
        damage = damageValue;
        Health -= damage;
    }

    private void DestroyThisTrash()
    {
        mainTrash.PlaySoundDestroyPieceTrash();
        mainTrash.DestroyPiece(this);        
        gameObject.SetActive(false);
    }

    public int GetTypeTrash()
    {
        return mainTrash.GetTypeTrash();
    }



}
