using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSizeCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private PlayerCleaning playerCleaning;

    [Header("For bonus crusher")]
    [SerializeField] private GameObject bonusCrusher;
    [SerializeField] private GameObject normalCrusher;
    [SerializeField] private BoxCollider bonusCollider;

    [Header("Change position and center collider")]
    [SerializeField] private float deltaCenterX = 0.004f;
    [SerializeField] private float deltaCenterZ = 0.074f;

    [SerializeField] private float deltaScaleX = 0.248f;
    [SerializeField] private float deltaScaleZ = 0.148f;

    public void ChangeCollider()
    {
        boxCollider.center = new Vector3(boxCollider.center.x + deltaCenterX,
                                         boxCollider.center.y,
                                         boxCollider.center.z + deltaCenterZ);

        boxCollider.size = new Vector3(boxCollider.size.x + deltaScaleX,
                                       boxCollider.size.y,
                                       boxCollider.size.z + deltaScaleZ);

        playerCleaning.GetTakeCollider().center = new Vector3(playerCleaning.GetTakeCollider().center.x + deltaCenterX,
                                                              playerCleaning.GetTakeCollider().center.y,
                                                              playerCleaning.GetTakeCollider().center.z + deltaCenterZ);

        playerCleaning.GetTakeCollider().size = new Vector3(playerCleaning.GetTakeCollider().size.x + deltaScaleX,
                                                            playerCleaning.GetTakeCollider().size.y,
                                                            playerCleaning.GetTakeCollider().size.z + deltaScaleZ);
    }

    public void BonusCrusherOn()
    {
        bonusCrusher.SetActive(true);
        normalCrusher.SetActive(false);
        bonusCollider.enabled = true;

        playerCleaning.EnableCrusherBonusCollider(true);
    }

    public void BonusCrusherOff()
    {
        bonusCrusher.SetActive(false);
        normalCrusher.SetActive(true);
        bonusCollider.enabled = false;
        playerCleaning.EnableCrusherBonusCollider(false);
    }
}
