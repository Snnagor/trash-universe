using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[Serializable]
public class ProductData
{
    [SerializeField] private string nameRecource;
    public string NameRecource { get => nameRecource; }

    [SerializeField] private Sprite iconResource;
    public Sprite IconResource { get => iconResource; }

    [SerializeField] private int costProductToResource;
    public int CostProductToResource { get => costProductToResource; }

    [SerializeField] private float costProductToMoney;
    public float CostProductToMoney { get => costProductToMoney; }

    [SerializeField] private int countProduct;
    public int CountProduct { get => countProduct; set => countProduct = value; }
}

public abstract class Resource : MonoBehaviour, IResource
{
    [SerializeField] private string nameRecource;
    public string NameRecource { get => nameRecource; }

    [SerializeField] private Sprite iconResource;
    public Sprite IconResource { get => iconResource; }

    [SerializeField] private float cost;
    public float Cost { get => cost;}

    [SerializeField] private ProductData[] products;
    public ProductData[] Products { get => products; }

    [SerializeField] private float timeRecycling;
    public float TimeRecycling { get => timeRecycling; }

    [SerializeField] private GameObject uiPanel;

    private int count;

    public int Count
    {
        get => count;
        set
        {
            count = value;
            StartRecycling();
            UIUpdateResourses(count);
        }
    }

    public int IdResource { get; set; }    

    private int currentCountRecycing;
    private float currentTimeRecycling = -2;    

    #region Injects

    protected SignalBus _signalBus;
    protected ViewModelRecycling _viewModelRecycling;
    protected ProductPlace _productPlace;
    protected Recycling _recycling;

    [Inject]
    private void Construct(SignalBus signalBus,
                           ViewModelRecycling viewModelRecycling,
                           ProductPlace productPlace,
                           Recycling recycling)
    {
        _signalBus = signalBus;
        _viewModelRecycling = viewModelRecycling;
        _productPlace = productPlace;
        _recycling = recycling;        
    }

    #endregion

    private void Start()
    {
        IdResource = Array.IndexOf(_recycling.Resources, this);

        UIUpdateInfo();
    }

    public void StartRecycling()
    {
        if (CanGoRecycling() && _productPlace.IsTherePlace() && currentTimeRecycling == -2)
        {
            currentTimeRecycling = TimeRecycling;
        }

        //if (Products[0].CountProduct > 0)
        //{
        //    VisibleUIPanel();
        //}
    }

    private void VisibleUIPanel()
    {
        if (!uiPanel.activeSelf)
        {
            uiPanel.SetActive(true);
        }

        UIUpdateProduct();
    }

    public void StopRecycling()
    {
        currentTimeRecycling = -2;
    }

    public void Execute()
    {
        if (currentTimeRecycling > 0)
        {
            currentTimeRecycling -= Time.deltaTime;
        }
        else if (currentTimeRecycling > -1)
        {
            Count--;
            currentCountRecycing++;

            if (currentCountRecycing == Products[0].CostProductToResource)
            {
                Products[0].CountProduct++;
                CreateProduct(IdResource, _productPlace.ProductContainer, _recycling.SizeProduct);
                UIUpdateProduct();
                currentCountRecycing = 0;
            }

            if (!CanGoRecycling() || !_productPlace.IsTherePlace())
            {
                StopRecycling();
            }
            else
            {
                currentTimeRecycling = TimeRecycling;
            }
        }
    }

    public void PutAwayProduct()
    {
        Products[0].CountProduct--;

        UIUpdateProduct();
    }

    public float GetCostProduct()
    {
        return Products[0].CostProductToMoney;
    }

    public abstract void UIUpdateInfo();

    public abstract void UIUpdateProduct();

    public abstract void UIUpdateResourses(int count);

    public abstract void CreateProduct(int idResource, Transform parent, float size);
    public abstract ProductPoolData CreateProductForTrailer(int idResource, Transform parent, float size);
    
    private bool CanGoRecycling()
    {
        return count >= Products[0].CostProductToResource;
    }
}
