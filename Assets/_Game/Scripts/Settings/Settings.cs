using UnityEngine;

[CreateAssetMenu(fileName = "New settings", menuName = "Create/Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] private AudioSource _BackgroundMusicPrefab;
    public AudioSource BackgroundMusicPrefab { get => _BackgroundMusicPrefab;}
   

    [Header("Upgrade Price")]

    [SerializeField] private int[] costCountStar;
    public int[] CostCountStar { get => costCountStar; }


    [SerializeField] private int[] costGroupStar;
    public int[] CostGroupStar { get => costGroupStar; }


    [SerializeField] private int[] costSizeStar;
    public int[] CostSizeStar { get => costSizeStar; }


    [SerializeField] private int[] costSpeedStar;
    public int[] CostSpeedStar { get => costSpeedStar; }


    [SerializeField] private int[] costSpaceTraile;
    public int[] CostSpaceTraile { get => costSpaceTraile; }


    [SerializeField] private int[] costSpeedWheels;
    public int[] CostSpeedWheels { get => costSpeedWheels;}    

    [Header("Cleaning Platform")]

    [SerializeField] private int maxPercentTrashes;
    public int MaxPercentTrashes { get => maxPercentTrashes;}

    [SerializeField] private int minPercentTrashes;
    public int MinPercentTrashes { get => minPercentTrashes; }

    [SerializeField] private int countHardForCompleteCleaning;
    public int CountHardForCompleteCleaning { get => countHardForCompleteCleaning;}
    

    [Header("Interstitials As")]

    [SerializeField] private float timeFirstShowAd;
    public float TimeFirstShowAd { get => timeFirstShowAd; }    

    [SerializeField] private float deltaTimeSeconShowAd;
    public float DeltaTimeSeconShowAd { get => deltaTimeSeconShowAd; }

}
