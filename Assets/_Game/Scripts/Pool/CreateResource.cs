using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaceCreatResources
{
    DestroyTrash,
    CreateAutotaker,
    LoadOnGround,
    LoadOnAutotakerPlace,
    LoadInTrailer
}

public class CreateResource : CreateItemFromPool
{           
    public TrashPackedData NewResource(Vector3 position, int type, PlaceCreatResources placeCreate, Transform parent, float size = 1f)
    {
        EnableAnotherItem();

        switch (placeCreate)
        {
            case PlaceCreatResources.DestroyTrash: (currentItem as TrashPackedData).SpawnForDestroyTrash(position, type, parent); break;
            case PlaceCreatResources.CreateAutotaker: (currentItem as TrashPackedData).SpawnForAutotakerConveyor(position, type, parent); break;
            case PlaceCreatResources.LoadOnGround: (currentItem as TrashPackedData).SpawnForLoadOnGround(position, type, parent); break;
            case PlaceCreatResources.LoadOnAutotakerPlace: (currentItem as TrashPackedData).SpawnForLoadAutotakerPlace(position, type, parent); break;
            case PlaceCreatResources.LoadInTrailer: (currentItem as TrashPackedData).SpawnForTraier(position, type, parent, size); break;
        }

        return currentItem as TrashPackedData;
    }
}
