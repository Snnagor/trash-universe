using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutotakerTrashInHand : MonoBehaviour
{
    [SerializeField] private Rigidbody[] trashOnHand;
    [SerializeField] private ParticleSystem destroyParticale;

    public ParticleSystem DestroyParticale { get => destroyParticale;}

    public void FallTrash()
    {
        foreach (var item in trashOnHand)
        {
            item.isKinematic = false;
        }
    }
}
