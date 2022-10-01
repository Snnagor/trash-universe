using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[Serializable]
public class TypeTrachPacked
{
    [SerializeField] private Material materials;
    public Material Materials { get => materials; }    

    [SerializeField] private float forceJump;
    public float ForceJump { get => forceJump; }    

}


public class TrashPackedData : ItemForSpawn
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private TypeTrachPacked[] typeTrachPacked;
   
    private TrashPacked _trashPacked;
    private AutotakerResourceMove _autotakerResourceMove;

    public int IndexType { get; set; }

    #region Injects

    private AutotakerControl _autotakerControl;

    [Inject]
    private void Construct(AutotakerControl autotakerControl)
    {
        _autotakerControl = autotakerControl;
    }

    #endregion

    private void Awake()
    {
        _trashPacked = GetComponent<TrashPacked>();
        _autotakerResourceMove = GetComponent<AutotakerResourceMove>();
    }

    public void DeactivateForCleaning()
    {
        _trashPacked.PreparationDeactivate();
        Deactivate();
    }

    public void Deactivate()
    {         
        transform.SetParent(RootParent); 
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }
       
    public void SpawnedFormPool(Vector3 pos, int type, float size = 1f)
    {                      
        
        transform.localPosition = pos;
        transform.localScale = new Vector3(size, size, size);
        IndexType = type;
        SetMaterial();    
    }

    public void SpawnForDestroyTrash(Vector3 pos, int type, Transform parent)
    {
        SpawnedFormPool(pos, type);        
        _trashPacked.EnablePhysics();
        _trashPacked.Jump(typeTrachPacked[IndexType].ForceJump);
        transform.SetParent(parent);
        _trashPacked.Collected = false;
    }

    public void SpawnForAutotakerConveyor(Vector3 pos, int type, Transform parent)
    {
        transform.eulerAngles = new Vector3(0, 45f, 0);
        
        transform.SetParent(parent);
        SpawnedFormPool(pos, type);  
        _trashPacked.MoveConveyor();
        _autotakerResourceMove.AnimationMovePathConveyor(_autotakerControl.PathConveyor);
    }

    public void SpawnForLoadOnGround(Vector3 pos, int type, Transform parent)
    {
        transform.SetParent(parent);
        SpawnedFormPool(pos, type);              
        _trashPacked.PropertyOnGround();        
    }

    public void SpawnForLoadAutotakerPlace(Vector3 pos, int type, Transform parent)
    {       
        transform.eulerAngles = new Vector3(0, 45f, 0);
        transform.SetParent(parent);
        SpawnedFormPool(_autotakerResourceMove.GetPlace(), type);
        _trashPacked.MoveConveyor();
        _autotakerResourceMove.AddProductInList();
        _autotakerResourceMove.Take();
    }

    public void SpawnForTraier(Vector3 pos, int type, Transform parent, float size)
    {
        transform.SetParent(parent);
        SpawnedFormPool(pos, type, size);
        _trashPacked.DisablePhysics();
        _trashPacked.Collected = true;
    }

    private void SetMaterial()
    {
        mesh.material = typeTrachPacked[IndexType].Materials;   
    }

    public bool GetIsCollected()
    {
        return _trashPacked.Collected;
    }

    public bool GetIsOnGround()
    {
        return _trashPacked.LiesOnGround;
    }

    public TrashPacked GetTrashPacked()
    {
        return _trashPacked;
    }
}
