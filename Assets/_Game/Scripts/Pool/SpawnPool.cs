using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

#region Resources

[System.Serializable]
public class ItemForPool
{
    [SerializeField] private int countResource;
    public int CountResource { get => countResource; private set => countResource = value; }

    [SerializeField] private ItemForSpawn prefab;
    public ItemForSpawn Prefab { get => prefab; private set => prefab = value; }
}

#endregion

public class SpawnPool : MonoBehaviour
{
    [SerializeField] private List<ItemForPool> items;
    [SerializeField] private List<ItemForSpawn> itemsOnScene;
    public List<ItemForSpawn> ItemsOnScene
    {
        get
        {
            return itemsOnScene;
        }
    }

    #region Injects

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    #endregion

    private void Awake()
    {
        CreateItems();
    }

    private void CreateItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < items[i].CountResource; j++)
            {
                ItemForSpawn newItem = Instantiate(items[i].Prefab, Vector3.zero, Quaternion.identity, transform);
                _diContainer.InjectGameObject(newItem.gameObject);

                newItem.RootParent = transform;

                itemsOnScene.Add(newItem);
                newItem.gameObject.SetActive(false);
            }
        }
    }
}
