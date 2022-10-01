using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class BonusMove : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider colliderBonus;
    public int IndexBonus { get; set; }

    #region Injects

    protected SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliderBonus = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360f, 0), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        Vector3 direction = new Vector3(Random.Range(-0.8f, 0.8f) * 5f, 10f, Random.Range(-0.8f, 0.8f) * 5f);

        rb.AddForce(direction, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Take"))
        {
            _signalBus.Fire(new BonusOpenUISignal { IndexBonus = IndexBonus });
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            colliderBonus.isTrigger = true;
        }


        if (collision.gameObject.CompareTag("Take"))
        {
            _signalBus.Fire(new BonusOpenUISignal { IndexBonus = IndexBonus });
            gameObject.SetActive(false);
        }
    }

   

}
