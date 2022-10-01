using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Place : MonoBehaviour
{
    [SerializeField] private Transform productContainer;
    public Transform ProductContainer { get => productContainer; }

    [SerializeField] private int maxCols;
    [SerializeField] private int maxRows;
    [SerializeField] private int maxHeight;

    private int currentCol, currentRow, currentHeight = 1;
    private int maxCountProduct;

    private void Start()
    {
        maxCountProduct = maxCols * maxRows * maxHeight;
    }

    public List<MoveToConveyor> AllItemConveyor { get; set; } = new List<MoveToConveyor>();

    public virtual void AddProductList(ProductMove product)
    {
        AllItemConveyor.Add(product);
    }

    public Vector3 GetPlaceForProduct()
    {
        Vector3 tmp = Vector3.up;

        if (AllItemConveyor.Count == 0)
        {
            currentCol++;
        }
        else
        {
            tmp = new Vector3(currentCol, currentHeight, currentRow * -1);

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

            if (currentHeight == maxHeight + 1)
            {
                //Stop recycling
                print("Stop recycling");
            }
        }

        return tmp;
    }

    public void PutAwayProduct()
    {
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
}
