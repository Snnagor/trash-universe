using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Recycling : MonoBehaviour
{
    [SerializeField] private Resource[] resources;
    public Resource[] Resources { get => resources; set => resources = value; }

    [SerializeField] private Transform startPathConveyor;
    public Transform StartPathConveyor { get => startPathConveyor; }

    [SerializeField] private Transform[] pathConveyor;
    public Vector3[] PathConveyor { get; set; }

    [SerializeField] private float sizeProduct;
    public float SizeProduct { get => sizeProduct; }

    #region Injects

    private ScoreManager _scoreManager;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(ScoreManager scoreManager,
                           SignalBus signalBus)
    {
        _scoreManager = scoreManager;
        _signalBus = signalBus;
    }

    #endregion

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<ScoreResourcesSignal>(ScoreResourcesSignal);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<ScoreResourcesSignal>(ScoreResourcesSignal);
    }

    private void ScoreResourcesSignal(ScoreResourcesSignal scoreResourcesSignal)
    {
        Resources[scoreResourcesSignal.Index].Count++;
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

    private void Update()
    {
        foreach (var item in Resources)
        {
            item.Execute();
        }
    }

    public void StartRecycling()
    {
        foreach (var item in Resources)
        {
            item.StartRecycling();
        }
    }

    public void StopRecycling()
    {
        foreach (var item in Resources)
        {
            item.StopRecycling();
        }
    }

    public List<RecyclingResourceDataForSave> SaveData()
    {
        List<RecyclingResourceDataForSave> CountResourcesRecyclingToSave = new List<RecyclingResourceDataForSave>();

        foreach (var item in resources)
        {
            CountResourcesRecyclingToSave.Add(new RecyclingResourceDataForSave(item.Count, item.Products[0].CountProduct));
        }

        return CountResourcesRecyclingToSave;
    }

    public void LoadData(List<RecyclingResourceDataForSave> dataResources)
    {
        for (int i = 0; i < dataResources.Count; i++)
        {
            Resources[i].Count = dataResources[i].CountResource;
            Resources[i].Products[0].CountProduct = dataResources[i].CountProduct;
        }

        StartRecycling();
    }
}
