using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItemFromPool : MonoBehaviour
{
    [SerializeField] protected SpawnPool spawnPool;

    public int CountItem { get; set; } = 0;

    protected ItemForSpawn currentItem;

    public void EnableAnotherItem()
    {
        if (CountItem >= spawnPool.ItemsOnScene.Count)
        {
            CountItem = 0;
        }

        currentItem = spawnPool.ItemsOnScene[CountItem];
        
        if (!currentItem.gameObject.activeSelf)
        {
            currentItem.gameObject.SetActive(true);
        }
        else
        {
            do
            {
                CountItem++;
                currentItem = spawnPool.ItemsOnScene[CountItem];
            }
            while (currentItem.gameObject.activeSelf);
        }

        CountItem++;
    }

    public ProductPoolData NewItem(int idResource, Transform parent, float size)
    {
        EnableAnotherItem();

        (currentItem as ProductPoolData).SpawnPoolForRecycling(idResource, parent, size);

        return currentItem as ProductPoolData;
    }

    public ProductPoolData NewItemForTrailer(int idResource, Transform parent, float size)
    {
        EnableAnotherItem();

        (currentItem as ProductPoolData).SpawnPoolForTrailer(idResource, parent, size);

        return currentItem as ProductPoolData;
    }
}
