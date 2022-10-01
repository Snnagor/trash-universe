using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Threading;
using System.Threading.Tasks;

[RequireComponent(typeof(IndicatorTrailer))]
public class TrailerTrash : MonoBehaviour
{
    public List<IItemTrailer> ItemInTrailer { get; set; } = new List<IItemTrailer>();
    public List<int> SaveTypeResourseInTrailer { get; set; } = new List<int>();
    public List<int> SaveTypeProductInTrailer { get; set; } = new List<int>();

    [SerializeField] private Transform trashContainer;
    [SerializeField] private GameObject fullTrailer;
    [SerializeField] private float deltaSizeFullTrailer;
    [SerializeField] private int maxCols;
    public int MaxCols { get => maxCols; }

    [SerializeField] private int maxRows;
    public int MaxRows { get => maxRows; }

    [SerializeField] private int maxHeight;
    public int MaxHeight { get => maxHeight; set => maxHeight = value; }

    [SerializeField] private float sizeTrashPackage;

    private RecyclingAnim factory;
    private Coroutine factoryCoroutine;
    private IndicatorTrailer indicatorTrailer;

    private int startLayer;
    public int CountMaxItemInTrailer { get => MaxCols * MaxRows * MaxHeight; }

    private float currentCol, currentRow, currentHeight = 1;

    private float startScaleFullTrailerY;
    public bool EmptyTrailer { get; set; } = true;
    public bool IsThereResource { get; set; } = false;
    public bool FullTrailer { get; set; }
    public float SizeTrashPackage { get => sizeTrashPackage; set => sizeTrashPackage = value; }

    #region Injects
    private SignalBus _signalBus;
    private CreateResource _createResource;
    private Recycling _recycling;

    [Inject]
    private void Construct(SignalBus signalBus,
                           CreateResource createResource,
                           Recycling recycling)
    {
        _signalBus = signalBus;
        _createResource = createResource;
        _recycling = recycling;
    }
    #endregion

    private void Awake()
    {
        indicatorTrailer = GetComponent<IndicatorTrailer>();
    }

    private void Start()
    {
        fullTrailer.SetActive(false);
        startLayer = gameObject.layer;
        startScaleFullTrailerY = fullTrailer.transform.localScale.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        MoveToFactory(other.gameObject);
        MoveToTrade(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        StopMoveFactory(other);
        StopMoveTrade(other);
    }

    private void StopMoveFactory(Collider other)
    {
        if (other.CompareTag("Factory") && factoryCoroutine != null)
        {
            StopCoroutine(factoryCoroutine);
            _signalBus.Fire(new CloseDoorRecyclingSignal());
            AfterUnloadingResources();
            SetFullTrailerTrue();
        }
    }

    private void StopMoveTrade(Collider other)
    {
        if (other.CompareTag("Trade") && factoryCoroutine != null)
        {
            StopCoroutine(factoryCoroutine);
            SetFullTrailerTrue();
        }
    }

    private void MoveToFactory(GameObject other)
    {
        if (other.CompareTag("Factory"))
        {
            if (IsThereResource)
            {
                _signalBus.Fire(new OpenDoorRecyclingSignal());

                SetFullTrailerFalse();
                var factory = other.GetComponent<RecyclingAnim>();
                factoryCoroutine = StartCoroutine(MoveToBuild(factory));
            }
        }
    }

    private void MoveToTrade(GameObject other)
    {
        if (other.CompareTag("Trade"))
        {
            if (!EmptyTrailer)
            {
                SetFullTrailerFalse();
                var trade = other.GetComponent<TradeTeleport>();
                factoryCoroutine = StartCoroutine(MoveToBuild(trade));
            }
        }
    }

    IEnumerator MoveToBuild(BuildAnimation factory)
    {
        for (int i = ItemInTrailer.Count - 1; i >= 0; i--)
        {
            if (factory is RecyclingAnim && !(ItemInTrailer[i] is TrashPacked)) continue;

            ItemInTrailer[i].Transform.SetParent(factory.transform);
            ItemInTrailer[i].AnimationMoveItemInBuild(ItemInTrailer[i].Transform.localPosition, factory.GetPlaceCollectedTrash(), factory.IdBuild);

            RemoveFromSaveList(i);

            ItemInTrailer.RemoveAt(i);

            indicatorTrailer.UpdateUIFullTrailer();

            UpdateCurrentPlace();

            yield return new WaitForSeconds(0.001f);
        }

        AfterUnloadingResources();
    }

    private void RemoveFromSaveList(int i)
    {
        if (ItemInTrailer[i].GetType() == typeof(TrashPacked))
        {
            SaveTypeResourseInTrailer.Remove(ItemInTrailer[i].IndexType);
        }
        else
        {
            SaveTypeProductInTrailer.Remove(ItemInTrailer[i].IndexType);
        }
    }

    private void UpdateCurrentPlace()
    {
        if (currentCol > 0)
        {
            currentCol--;
        }
        else if (currentCol == 0 && currentRow > 0)
        {
            currentCol = MaxCols - 1;
            currentRow--;
        }
        else if (currentCol == 0 && currentRow == 0 && currentHeight > 1)
        {
            currentCol = MaxCols - 1;
            currentRow = MaxRows - 1;
            currentHeight--;
        }
    }

    public void TrashContainer(Transform trash)
    {
        trash.SetParent(trashContainer);
    }

    public void AddItemInTrailerList(IItemTrailer trash)
    {
        if (EmptyTrailer)
            EmptyTrailer = false;

        if (trash.GetType() == typeof(TrashPacked))
        {
            if (!IsThereResource)
            {
                IsThereResource = true;
            }

            SaveTypeResourseInTrailer.Add(trash.IndexType);
        }
        else
        {
            SaveTypeProductInTrailer.Add(trash.IndexType);
        }

        ItemInTrailer.Add(trash);

        indicatorTrailer.UpdateUIFullTrailer();
    }

    public void UpdateIndicatorTrailer()
    {
        indicatorTrailer.UpdateUIFullTrailer();
    }

    public Vector3 GetCurrentPosition()
    {
        Vector3 tmpPos = Vector3.up * SizeTrashPackage;

        if (ItemInTrailer.Count == 0)
        {
            currentCol++;
        }
        else
        {
            tmpPos = new Vector3(currentCol, currentHeight, currentRow * -1) * SizeTrashPackage;

            if (currentCol == MaxCols - 1)
            {
                currentCol = 0;
                currentRow++;
            }
            else
            {
                currentCol++;
            }

            if (currentRow == MaxRows)
            {
                currentCol = 0;
                currentRow = 0;
                currentHeight++;
            }

            if (currentHeight == MaxHeight + 1)
            {
                FullTrailerActivate();
            }
        }

        return tmpPos;
    }

    private void FullTrailerActivate()
    {
        fullTrailer.SetActive(true);

        Vector3 newScale = fullTrailer.transform.localScale;

        float maxSizeFullTrailerY = currentHeight * (sizeTrashPackage);

        newScale = new Vector3(fullTrailer.transform.localScale.x, startScaleFullTrailerY + maxSizeFullTrailerY, fullTrailer.transform.localScale.z);

        fullTrailer.transform.localScale = newScale;
        FullTrailer = true;
        gameObject.layer = LayerMask.NameToLayer("Full");
    }

    private void AfterUnloadingResources()
    {
        currentCol = 0;
        currentRow = 0;
        currentHeight = 1;
        IsThereResource = false;

        if (ItemInTrailer.Count == 0)
        {
            FullTrailer = false;
            EmptyTrailer = true;
        }
        else
        {
            foreach (var item in ItemInTrailer)
            {
                item.Position = GetCurrentPosition();

                if (item is TrashPacked && !IsThereResource)
                {
                    IsThereResource = true;
                }
            }
        }
    }        

    public void SetFullTrailerFalse()
    {
        if (FullTrailer)
        {
            FullTrailer = false;
            fullTrailer.SetActive(false);
            gameObject.layer = startLayer;
        }
    }

    public void SetFullTrailerTrue()
    {
        if (currentHeight >= MaxHeight + 1)
        {
            FullTrailerActivate();
        }
    }

    public void LoadItem(List<int> loadTypeResources, List<int> loadTypeProduct)
    {        
        if (loadTypeResources.Count > 0)
        {            
            for (int i = 0; i < loadTypeResources.Count; i++) 
            {               
                var position = GetCurrentPosition();
                var newResource = _createResource.NewResource(position,
                                             loadTypeResources[i],
                                             PlaceCreatResources.LoadInTrailer,
                                             trashContainer,
                                             SizeTrashPackage);
                
                AddItemInTrailerList(newResource.GetTrashPacked());
            }
        }

        

        if (loadTypeProduct.Count > 0)
        {            
            for (int i = 0; i < loadTypeProduct.Count; i++)
            {
                var position = GetCurrentPosition(); 
                int idResource = loadTypeProduct[i];
              
                var newProduct = _recycling.Resources[idResource].CreateProductForTrailer(idResource, trashContainer, SizeTrashPackage);

                newProduct.transform.localPosition = position;

                AddItemInTrailerList(newProduct.GetProducMove());
            }
        }
    }
}
