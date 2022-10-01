using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PlayerMoveInUpgrade : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float rotateY;
    [SerializeField] private float duration;
    [SerializeField] private float deltaChangeDuration = 0.05f;

    #region Injects

    private TutorialManager _tutorialManager;
    private SoundManager _soundManager;

    [Inject]
    private void Construct(TutorialManager tutorialManager,
                           SoundManager soundManager)
    {
        _tutorialManager = tutorialManager;
        _soundManager = soundManager;
    }

    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Upgrade"))
        {            
            Move();

            _soundManager.UpgradeSound();

            if (_tutorialManager.TutorialUpgrade)
            {
                _tutorialManager.TutorialUpgrade = false;
            }
        }
    }

    private void Move()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(endPosition, duration));
        seq.Join(transform.DORotate(new Vector3(0, rotateY, 0), duration));
    }

    public void ChangeDurationMove()
    {
        duration -= deltaChangeDuration;
    }
}
