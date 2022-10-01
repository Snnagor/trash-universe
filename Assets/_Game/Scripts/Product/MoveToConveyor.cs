using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public abstract class MoveToConveyor : MonoBehaviour
{
    protected ProductPoolData data;

    public Transform Transform { get => transform; }
    public Vector3 LocalPosition { get => transform.localPosition; }

    private Sequence seq;

    public bool CanTake { get; set; }

    public int IndexType => data.IdResource;

    #region Injects        

    protected ScoreManager _scoreManager;
    protected TradeTeleport _tradeTeleport;
    private SoundManager _soundManager;

    [Inject]
    private void Construct(ScoreManager scoreManager,
                           TradeTeleport tradeTeleport,
                           SoundManager soundManager)
    {
        _scoreManager = scoreManager;
        _tradeTeleport = tradeTeleport;
        _soundManager = soundManager;
    }

    #endregion

    private void Awake()
    {
        data = GetComponent<ProductPoolData>();
    }

    public virtual void AnimationMovePathConveyor(Vector3[] _path)
    {
        AddProductInList();

        seq = DOTween.Sequence();
        seq.Append(transform.DOLocalPath(_path, 1f, PathType.CatmullRom, PathMode.Full3D, 10).OnComplete(Take));
        seq.Append(transform.DOLocalMove(GetPlace(), 0.5f));
    }

    public void KillSeq()
    {
        seq.Kill();
    }

    public void Take()
    {
        CanTake = true;
    }

    public abstract Vector3 GetPlace();

    public abstract void AddProductInList();

    public abstract IItemTrailer GetIItemTrailer();

    public virtual void AnimationMoveToTrailer(Vector3 _startPosition, Vector3 _endPosition, float _sizeTrashPackage)
    {
        Vector3[] Path = new Vector3[2];

        Vector3 middlePath = Vector3.Lerp(_startPosition, _endPosition, 0.5f);
        Path[0] = new Vector3(middlePath.x, middlePath.y + 10f, middlePath.z);
        Path[1] = _endPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalPath(Path, 0.5f, PathType.CatmullRom, PathMode.Full3D, 10).SetEase(Ease.InOutQuad).OnComplete(FinishedMoveToTrailer));
        seq.Join(transform.DOLocalRotate(Vector3.zero, 0.5f));
        seq.Join(transform.DOScale(_sizeTrashPackage, 0.5f).SetEase(Ease.Linear));
    }

    private void FinishedMoveToTrailer()
    {
        _soundManager.TakeResource();
    }
}
