using UnityEngine;
using DG.Tweening;
using Zenject;


[RequireComponent(typeof(TrashPackedData))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class TrashPacked : MonoBehaviour, IItemTrailer
{
    [SerializeField] private float speedCollected = 0.3f;

    private TrashPackedData data;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private Vector3 StartPosition;
    private Vector3 TargetPosition;

    public bool Collected { get; set; }
    public bool LiesOnGround { get; set; }
    public float Cost => _recycling.Resources[data.IndexType].Cost;
    public Transform Transform { get => transform; }
    public Vector3 Position
    {
        set
        {
            transform.localPosition = value;
        }
    }

    public int IndexType => data.IndexType;

    #region Injects

    private SignalBus _signalBus;
    private ScoreManager _scoreManager;
    private TradeTeleport _tradeTeleport;
    private Recycling _recycling;
    private SoundManager _soundManager;

    [Inject]
    private void Construct(SignalBus signalBus,
                           ScoreManager scoreManager,
                           TradeTeleport tradeTeleport,
                           Recycling recycling,
                           SoundManager soundManager)
    {
        _signalBus = signalBus;
        _scoreManager = scoreManager;
        _tradeTeleport = tradeTeleport;
        _recycling = recycling;
        _soundManager = soundManager;
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PropertyOnGround();
        }
    }    

    private void Awake()
    {
        data = GetComponent<TrashPackedData>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Jump(float forceJump)
    {
        EnablePhysics();

        float dirX = Random.Range(-1f, 1f);
        float dirZ = Random.Range(-1f, 1f);

        Vector3 direction = new Vector3(dirX, 1, dirZ) * forceJump;

        rb.AddForce(direction, ForceMode.Impulse);
        rb.AddTorque(direction, ForceMode.Impulse);
    }

    public void MoveConveyor()
    {        
        DisablePhysics();
        Collected = true;
    }

    public void MoveTrashPackedInTrailer(TrailerTrash trailer)
    {
        DisablePhysics();
        
        if (Collected == true) return;
        Collected = true;

        _signalBus.Fire(new TookPackedTrashSignal());

        trailer.TrashContainer(transform);

        trailer.AddItemInTrailerList(this);

        TargetPosition = trailer.GetCurrentPosition();
        StartPosition = transform.localPosition;
        AnimationTrashCollected(StartPosition, TargetPosition, trailer.SizeTrashPackage);
    }

    public void AnimationTrashCollected(Vector3 _startPosition, Vector3 _endPosition, float _sizeTrashPackage)
    {
        Vector3[] Path = new Vector3[2];

        Vector3 middlePath = Vector3.Lerp(_startPosition, _endPosition, 0.5f);

        Path[0] = new Vector3(middlePath.x, middlePath.y + 8f, middlePath.z);
        Path[1] = _endPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalPath(Path, 0.8f, PathType.CatmullRom, PathMode.Full3D, 10).SetEase(Ease.InOutQuad).OnComplete(FinishMoveToTrailer));
        seq.Join(transform.DOLocalRotate(Vector3.zero, 0.8f));
        seq.Join(transform.DOScale(_sizeTrashPackage, 0.8f).SetEase(Ease.Linear));
    }

    public void FinishMoveToTrailer()
    {       
        _soundManager.TakeResource();
        DisablePhysics();
    }

    public void DisablePhysics()
    {       
        // rb.mass = 0.00001f;
        if (!rb.isKinematic) rb.isKinematic = true;
        if (rb.detectCollisions) rb.detectCollisions = false;
        if (boxCollider.enabled) boxCollider.enabled = false;
    }

    public void EnablePhysics()
    {       
        if (!boxCollider.enabled) boxCollider.enabled = true;
        if (rb.isKinematic) rb.isKinematic = false;
        if (!rb.detectCollisions) rb.detectCollisions = true;        
    }

    public void PropertyOnGround()
    {
        if (!boxCollider.enabled) boxCollider.enabled = true;
        rb.isKinematic = true;
        boxCollider.isTrigger = true;
        LiesOnGround = true;
        Collected = false;
    }

    public void AnimationMoveItemInBuild(Vector3 _startPosition, Vector3 _endPosition, int idBuild)
    {
        Vector3[] Path = new Vector3[2];

        Vector3 middlePath = Vector3.Lerp(_startPosition, _endPosition, 0.5f);

        Path[0] = new Vector3(middlePath.x, middlePath.y + 10f, middlePath.z);
        Path[1] = _endPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalPath(Path, 1f, PathType.CatmullRom, PathMode.Full3D, 10).SetEase(Ease.InOutQuad).OnComplete(() => DeactivateFactory(idBuild)));
        seq.Join(transform.DOScale(1f, 1f));
    }

    public void DeactivateFactory(int idBuild)
    {
        switch (idBuild)
        {
            case 0: TradeTeleport(); break;
            case 1: RecyclingFactory(); break;
        }

        Deactivate();
    }

    private void RecyclingFactory()
    {
        _signalBus.Fire(new ScoreResourcesSignal { Index = data.IndexType });
    }

    private void TradeTeleport()
    {
        _scoreManager.TmpMoney += Cost;
        _tradeTeleport.FlashPlay();
    }

    public void PreparationDeactivate()
    {
        boxCollider.isTrigger = false;
        LiesOnGround = false;
        DisablePhysics();
    }

    private void Deactivate()
    {
        PreparationDeactivate();
        data.Deactivate();
    }

}

