using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlace : MonoBehaviour
{
    [SerializeField] private Recycling _recycling;
    [SerializeField] private Transform productContainer;
    public Transform ProductContainer { get => productContainer; }

    [SerializeField] private int maxCols;
    [SerializeField] private int maxRows;
    [SerializeField] private int maxHeight;
    [SerializeField] protected GameObject fullTrailer;
    [SerializeField] private float minScaleY;

    private int currentCol, currentRow, currentHeight = 1;
    private int maxCountProduct;

    private void Start()
    {
        maxCountProduct = maxCols * maxRows * maxHeight;
    }

    public List<MoveToConveyor> AllItemConveyor { get; set; } = new List<MoveToConveyor>();

    public virtual void AddProductList(MoveToConveyor product)
    {
        AllItemConveyor.Add(product);
        EnableFullIndicator();
    }

    private void EnableFullIndicator()
    {
        if (maxCountProduct == AllItemConveyor.Count && !fullTrailer.activeSelf)
        {
            fullTrailer.SetActive(true);
            fullTrailer.transform.localScale = new Vector3(fullTrailer.transform.localScale.x,
                                                           minScaleY * maxHeight, 
                                                           fullTrailer.transform.localScale.z);
        }
    }

    public virtual Vector3 GetPlaceForProduct(float size)
    {
        Vector3 tmp = Vector3.up * size;

        if (AllItemConveyor.Count == 0)
        {
            currentCol++;
        }
        else
        {
            tmp = new Vector3(currentCol, currentHeight, currentRow * -1) * size;

            if (currentCol == maxCols - 1)
            {
                currentCol = 0;
                currentRow++;
            }
            else
            {
                currentCol++;
            }

            if (currentRow == maxRows)
            {
                currentCol = 0;
                currentRow = 0;
                currentHeight++;
            }
           
        }

        return tmp;
    }

    public void PutAwayProduct()
    {
        if (fullTrailer.activeSelf)
            fullTrailer.SetActive(false);

        if (currentCol > 0)
        {
            currentCol--;
        }
        else if (currentCol == 0 && currentRow > 0)
        {
            currentCol = maxCols - 1;
            currentRow--;
        }
        else if (currentCol == 0 && currentRow == 0 && currentHeight > 1)
        {
            currentCol = maxCols - 1;
            currentRow = maxRows - 1;
            currentHeight--;
        }
    }

    public bool IsTherePlace()
    {
        return AllItemConveyor.Count < maxCountProduct;
    }

    public virtual void RemoveItemFromList(int i)
    {
        AllItemConveyor.RemoveAt(i);
    }

    public List<int> SaveData()
    {
        List<int> tmpTypeProducts = new List<int>();

        if(AllItemConveyor.Count > 0)
        {
            foreach (var item in AllItemConveyor)
            {
                tmpTypeProducts.Add(item.IndexType);
            }
        }

        return tmpTypeProducts;
    }

    public void LoadData(List<int> loadTypeProduct)
    {
        if(loadTypeProduct.Count > 0)
        {
            for (int i = 0; i < loadTypeProduct.Count; i++)
            {
                float sizeProduct = _recycling.SizeProduct;
                int idResource = loadTypeProduct[i];
                var position = GetPlaceForProduct(sizeProduct);

                var newProduct = _recycling.Resources[idResource].CreateProductForTrailer(idResource, productContainer, sizeProduct);

                newProduct.transform.localPosition = position;

                newProduct.GetProducMove().Take();

                AddProductList(newProduct.GetProducMove());
            }
        }
    }
}
