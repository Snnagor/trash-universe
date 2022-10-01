using UnityEngine;
using Zenject;

public class AutotakerControl : MonoBehaviour
{
    [SerializeField] private AutotakerTrashControl trashControl;
    [SerializeField] private ResourcePlace resourcePlace;
    [SerializeField] private float timeMiningOneResorce;

    [SerializeField] private Transform startPathConveyor;
    public Transform StartPathConveyor { get => startPathConveyor; }

    [SerializeField] private Transform[] pathConveyor;
    
    private int _indexCurrentTypeTrash;

    public Vector3[] PathConveyor { get; set; }
    public int CurrentCreateResource { get; set; }
    public bool WorkAutotraker { get; set; }    

    private float _currentMiningTime = -2;

    #region Injects
     
    private CreateResource _createResource;
    private TutorialManager _tutorialManager;

    [Inject]
    private void Construct(CreateResource createResource,
                           TutorialManager tutorialManager)
    {               
        _createResource = createResource;
        _tutorialManager = tutorialManager;
    }

    #endregion

    private void Awake()
    {
        PathConveyor = new Vector3[pathConveyor.Length];
    }

    private void Start()
    {
        FillPathConveyor();
    }

    private void FillPathConveyor()
    {
        for (int i = 0; i < pathConveyor.Length; i++)
        {
            PathConveyor[i] = pathConveyor[i].localPosition;
        }
    }

    public void StartMiningResource(enumTypeTrash currentTypeTrash)
    {
        if (!WorkAutotraker && resourcePlace.IsTherePlace())
        {
            _currentMiningTime = timeMiningOneResorce;
            this._indexCurrentTypeTrash = currentTypeTrash.GetHashCode();
            WorkAutotraker = true;            
        }
    }

    public void StartMiningAfterPauseFullPlace()
    {        
        if (!WorkAutotraker && 
            resourcePlace.IsTherePlace() && 
            CurrentCreateResource != 0 )
        {
            if (CurrentCreateResource < trashControl.CountResourceInOneTake)
            {
                _currentMiningTime = timeMiningOneResorce;                
            }
            else
            {
                trashControl.OnTakeTrash();
            }

            WorkAutotraker = true;
        }
    }


    private void Update()
    {
        if (_currentMiningTime > 0)
        {
            _currentMiningTime -= Time.deltaTime;
        }
        else if (_currentMiningTime > -1)
        {
            _createResource.NewResource(startPathConveyor.localPosition, 
                                        _indexCurrentTypeTrash, 
                                        PlaceCreatResources.CreateAutotaker, 
                                        resourcePlace.ProductContainer);
          
            CurrentCreateResource++;            

            if (CurrentCreateResource < trashControl.CountResourceInOneTake)
            {
                _currentMiningTime = timeMiningOneResorce;

                if (!resourcePlace.IsTherePlace())
                {
                    StopAutotaker();
                }
            }
            else
            {
                StopAutotaker();
                CurrentCreateResource = 0;
                trashControl.ChangePlatform();
                trashControl.OnTakeTrash();                
            }
        }
    }

    public void StopAutotaker()
    {
        _currentMiningTime = -2;
        WorkAutotraker = false;
    }

    public void LoadResourceFormSave(int createCurrentResource, int[] typeTrash, int idCurrentCreateTypeResource)
    {
        for (int i = 0; i < typeTrash.Length; i++)
        {            
            _createResource.NewResource(startPathConveyor.localPosition,
                                        typeTrash[i],
                                        PlaceCreatResources.LoadOnAutotakerPlace,
                                        resourcePlace.ProductContainer);
        }

        CurrentCreateResource = createCurrentResource;        

        _indexCurrentTypeTrash = idCurrentCreateTypeResource;

        StartMiningAfterPauseFullPlace();
    }
}
