using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTrashPaper : CreateItemFromPool
{
    public TrashData NewItem(Vector3 pos, Transform parent, int countResources, float rotation)
    {
        EnableAnotherItem();

        (currentItem as TrashData).OnSpawnedPool(pos, parent, countResources, rotation);

        return currentItem as TrashData;
    }
}
