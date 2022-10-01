using UnityEngine;

[CreateAssetMenu(fileName = "New settings Shop", menuName = "Create/SettingsShop")]
public class SettingsShop : ScriptableObject
{
    [Header("JunkShop Set")]

    [SerializeField] private float junkBotPercentDeltaSpeed;
    public float JunkBotPercentDeltaSpeed { get => junkBotPercentDeltaSpeed; }
   
    [SerializeField] private int junkBotCountCrystals;
    public int JunkBotCountCrystals { get => junkBotCountCrystals; }

    [SerializeField] private double junkBotCostMoney;
    public double JunkBotCostMoney { get => junkBotCostMoney; }

    [SerializeField] private string junkBotProductID;
    public string JunkBotProductID { get => junkBotProductID; }

    [Header("Every Day")]

    [SerializeField] private int everyDayCrystals;
    public int EveryDayCrystals { get => everyDayCrystals;}

    [SerializeField] private float everyDayDeltaTime = 86400000;
    public float EveryDayDeltaTime { get => everyDayDeltaTime; }

    [Header("Special Offer")]

    [SerializeField] private int specialOfferCristals;
    public int SpecialOfferCristals { get => specialOfferCristals;}

    [SerializeField] private int specialOfferCoins;
    public int SpecialOfferCoins { get => specialOfferCoins; }

    [SerializeField] private double specialOfferCostMoney;
    public double SpecialOfferCostMoney { get => specialOfferCostMoney; }

    [SerializeField] private string specialOfferProductID;
    public string SpecialOfferProductID { get => specialOfferProductID; }

    [Header("Small Coins")]

    [SerializeField] private int smallCoins;
    public int SmallCoins { get => smallCoins;}

    [SerializeField] private int smallCoinsCost;
    public int SmallCoinsCost { get => smallCoinsCost; }

    [Header("Middle Coins")]

    [SerializeField] private int middleCoins;
    public int MiddleCoins { get => middleCoins; }

    [SerializeField] private int middleCoinsCost;
    public int MiddleCoinsCost { get => middleCoinsCost; }

    [Header("Big Coins")]

    [SerializeField] private int bigCoins;
    public int BigCoins { get => bigCoins; }

    [SerializeField] private int bigCoinsCost;
    public int BigCoinsCost { get => bigCoinsCost; }

    [Header("Small Crystals")]

    [SerializeField] private int smallCrystals;
    public int SmallCrystals { get => smallCrystals; }

    [SerializeField] private double smallCrystalsCostMoney;
    public double SmallCrystalsCostMoney { get => smallCrystalsCostMoney; }

    [SerializeField] private string smallCrystalsProductID;
    public string SmallCrystalsProductID { get => smallCrystalsProductID; }

    [Header("Middle Crystals")]

    [SerializeField] private int middleCrystals;
    public int MiddleCrystals { get => middleCrystals; }

    [SerializeField] private double middleCrystalsCostMoney;
    public double MiddleCrystalsCostMoney { get => middleCrystalsCostMoney; }

    [SerializeField] private string middleCrystalsProductID;
    public string MiddleCrystalsProductID { get => middleCrystalsProductID; }

    [Header("Big Crystals")]

    [SerializeField] private int bigCrystals;
    public int BigCrystals { get => bigCrystals; }

    [SerializeField] private double bigCrystalsCostMoney;
    public double BigCrystalsCostMoney { get => bigCrystalsCostMoney; }

    [Header("Boost Crusher")]

    [SerializeField] private double boostCrusherCostMoney;
    public double BoostCrusherCostMoney { get => boostCrusherCostMoney; }

    [Header("Boost Force")]

    [SerializeField] private double boostForceCostMoney;
    public double BoostForceCostMoney { get => boostForceCostMoney; }

    [Header("Boost Trailer")]

    [SerializeField] private double boostTrailerCostMoney;
    public double BoostTrailerCostMoney { get => boostTrailerCostMoney; }

    [Header("Boost Magnet")]

    [SerializeField] private double boostMagnetCostMoney;
    public double BoostMagnetCostMoney { get => boostMagnetCostMoney; }

    [Header("True No Ads")]

    [SerializeField] private double trueNoAdsCostMoney;
    public double TrueNoAdsCostMoney { get => trueNoAdsCostMoney; }

    [Header("One-Time Offer")]

    [SerializeField] private float timeBetweenOffers;
    public float TimeBetweenOffers { get => timeBetweenOffers; set => timeBetweenOffers = value; }

    [SerializeField] private int countCoinOneTimeOffer;
    public int CountCoinOneTimeOffer { get => countCoinOneTimeOffer; set => countCoinOneTimeOffer = value; }

    [SerializeField] private int countCrystalsOneTimeOffer;
    public int CountCrystalsOneTimeOffer { get => countCrystalsOneTimeOffer; set => countCrystalsOneTimeOffer = value; }    

    [SerializeField] private double oldCostOneTimeOffer;
    public double OldCostOneTimeOffer { get => oldCostOneTimeOffer; set => oldCostOneTimeOffer = value; }

    [SerializeField] private double costOneTimeOffer;
    public double CostOneTimeOffer { get => costOneTimeOffer; set => costOneTimeOffer = value; }
    
}
