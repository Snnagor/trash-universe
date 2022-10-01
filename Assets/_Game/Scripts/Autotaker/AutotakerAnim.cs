using UnityEngine;

public class AutotakerAnim : MonoBehaviour
{
    [SerializeField] private Animator animHand;
    [SerializeField] private Animator animBase;

    public void OnWork()
    {
        animBase.SetBool("On", true);
    }

    public void OffWork()
    {
        animBase.SetBool("On", false);
    }

    public void OnHand()
    {
        animHand.SetBool("Take", true);
    }

    public void OffHand()
    {
        animHand.SetBool("Take", false);
    }
}
