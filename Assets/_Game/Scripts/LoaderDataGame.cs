using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoaderDataGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;

    #region Injects

    private SerialDataManager _serialDataManager;
    private PlatformMainTrashControl _platformMainTrashControl;
    private AutotakerTrashControl _autotakerTrashControl;
    private PlatformRecyclingMove _platformRecyclingMove;
    private PlatformAutotakerMove _platformAutotakerMove;
    private PlatformManager _platformManager;
    private TrailerTrash _trailerTrash;
    private Recycling _recycling;
    private ProductPlace _productPlace;
    private SoundManager _soundManager;
    private TutorialManager _tutorialManager;
    private BonusesManager _bonusesManager;
    private PurchaseControl _purchaseControl;
    private AnalyticsTimerService _analyticsTimerService;
    private BusMove _busMove;
    private IndicatorTrashInPlatform _indicatorTrashInPlatform;

    [Inject]
    private void Construct(SerialDataManager serialDataManager,
                           PlatformMainTrashControl platformMainTrashControl,
                           PlatformRecyclingMove platformRecyclingMove,
                           PlatformAutotakerMove platformAutotakerMove,
                           PlatformManager platformManager,
                           AutotakerTrashControl autotakerTrashControl,
                           TrailerTrash trailerTrash,
                           Recycling recycling,
                           ProductPlace productPlace,
                           SoundManager soundManager,
                           TutorialManager tutorialManager,
                           BonusesManager bonusesManager,
                           PurchaseControl purchaseControl,
                           AnalyticsTimerService analyticsTimerService,
                           BusMove busMove,
                           IndicatorTrashInPlatform indicatorTrashInPlatform)
    {
        _serialDataManager = serialDataManager;
        _platformMainTrashControl = platformMainTrashControl;
        _platformRecyclingMove = platformRecyclingMove;
        _platformAutotakerMove = platformAutotakerMove;
        _platformManager = platformManager;
        _autotakerTrashControl = autotakerTrashControl;
        _trailerTrash = trailerTrash;
        _recycling = recycling;
        _productPlace = productPlace;
        _soundManager = soundManager;
        _tutorialManager = tutorialManager;
        _bonusesManager = bonusesManager;
        _purchaseControl = purchaseControl;
        _analyticsTimerService = analyticsTimerService;
        _busMove = busMove;
        _indicatorTrashInPlatform = indicatorTrashInPlatform;
    }

    #endregion


    public void StartLocal()
    {
        _platformManager.CurrentRecyclingPlatformMoney = _serialDataManager.Data.CurrentPaidMoneyRecycling;
        _platformManager.CurrentAutotakerPlatformMoney = _serialDataManager.Data.CurrentPaidMoneyAutotaker;
        _platformManager.CurrentNextLevelMoney = _serialDataManager.Data.CurrentPaidMoneyNextLevel;

        LoadItemInTrailer();

        if (_platformRecyclingMove.Built)
        {
            _recycling.LoadData(_serialDataManager.Data.SavedResourcesRecycling);
            _productPlace.LoadData(_serialDataManager.Data.SavesProductTypeInPlace);
        }

        _soundManager.LoadMusicAndSound();
    }

    public void InitGameData()
    {
        scoreManager.TmpMoney = _serialDataManager.Data.TotalMoney;
        scoreManager.TotalHard = _serialDataManager.Data.TotalHard;

        gameManager.IdCurrentPlatform = _serialDataManager.Data.IdCurrentPlatform;
        gameManager.AnalyticsCountPlatform = _serialDataManager.Data.AnalyticsCountPlatform;

        gameManager.CurrentIncreaseResisTrash = _serialDataManager.Data.CurrentIncreaseResisTrash;

        _platformRecyclingMove.Built = _serialDataManager.Data.BuiltRecyclilng;

        _platformAutotakerMove.Built = _serialDataManager.Data.BuiltAutotaker;

        _busMove.BusArrival = _serialDataManager.Data.BusArrival;

        _bonusesManager.FirstBonus = _serialDataManager.Data.FirstBonus;
        _tutorialManager.TutorialControl = _serialDataManager.Data.TutorialControl;
        _tutorialManager.TutorialTeleport = _serialDataManager.Data.TutorialTeleport;
        _tutorialManager.TutorialRecyclingAutotaker = _serialDataManager.Data.TutorialRecyclingAutotaker;
        _tutorialManager.TutorialUpgrade = _serialDataManager.Data.TutorialUpgrade;
        _tutorialManager.TutorialRecycling = _serialDataManager.Data.TutorialRecycling;
        _tutorialManager.TutorialFullAutotaker = _serialDataManager.Data.TutorialFullAutotaker;
        _tutorialManager.TutorialNextLevel = _serialDataManager.Data.TutorialNextLevel;
        _tutorialManager.TutorialAutotakerFirstTime = _serialDataManager.Data.TutorialAutotakerFirstTime;

        _purchaseControl.AnalyticsFirstPurchase = _serialDataManager.Data.AnalyticsFirsPurchase;
    }

    public void PurchaseLoad()
    {
        _purchaseControl.SpecialOffer = _serialDataManager.Data.SpecialOffer;
        _purchaseControl.OneTimeOffer = _serialDataManager.Data.OneTimeOffer;
        _purchaseControl.NoAdsBoost = _serialDataManager.Data.NoAdsBoost;

        _purchaseControl.JunkBotSet = _serialDataManager.Data.JunkBotSet;

        _purchaseControl.ForceBoost = _serialDataManager.Data.ForceBoost;
        _purchaseControl.CrusherBoost = _serialDataManager.Data.CrusherBoost;
        _purchaseControl.TrailerBoost = _serialDataManager.Data.TrailerBoost;
        _purchaseControl.MagnetBoost = _serialDataManager.Data.MagnetBoost;
    }

    private void LoadItemInTrailer()
    {
        _trailerTrash.LoadItem(_serialDataManager.Data.SavesTypeResoursesTrailer, _serialDataManager.Data.SavesTypeProductTrailer);
        _serialDataManager.ClearDataSavesItemInTrailer();
    }

    private void OnApplicationPause(bool value)
    {
        if (value)
        {
            SaveAllData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveAllData();
    }
       

    public void SaveAllData()
    {
        _serialDataManager.Data.TotalMoney = scoreManager.TotalMoney;
        _serialDataManager.Data.TotalHard = scoreManager.TotalHard;

        _serialDataManager.Data.IdCurrentPlatform = gameManager.IdCurrentPlatform;
        _serialDataManager.Data.CurrentIncreaseResisTrash = gameManager.CurrentIncreaseResisTrash;

        _serialDataManager.Data.BuiltRecyclilng = _platformRecyclingMove.Built;
        _serialDataManager.Data.CurrentPaidMoneyRecycling = _platformManager.CurrentRecyclingPlatformMoney;

        _serialDataManager.Data.BuiltAutotaker = _platformAutotakerMove.Built;
        _serialDataManager.Data.CurrentPaidMoneyAutotaker = _platformManager.CurrentAutotakerPlatformMoney;

        _serialDataManager.Data.BusArrival = _busMove.BusArrival;
        _serialDataManager.Data.CurrentPaidMoneyNextLevel = _platformManager.CurrentNextLevelMoney;

        _platformMainTrashControl.SaveMainTrahDestroy();
        _autotakerTrashControl.SaveAutotakerTrahDestroy();

        _serialDataManager.Data.SavesTypeResoursesTrailer = _trailerTrash.SaveTypeResourseInTrailer;
        _serialDataManager.Data.SavesTypeProductTrailer = _trailerTrash.SaveTypeProductInTrailer;

        _serialDataManager.Data.SavedResourcesRecycling = _recycling.SaveData();
        _serialDataManager.Data.SavesProductTypeInPlace = _productPlace.SaveData();

        _serialDataManager.Data.FirstBonus = _bonusesManager.FirstBonus;
        _serialDataManager.Data.TutorialControl = _tutorialManager.TutorialControl;
        _serialDataManager.Data.TutorialTeleport = _tutorialManager.TutorialTeleport;
        _serialDataManager.Data.TutorialRecyclingAutotaker = _tutorialManager.TutorialRecyclingAutotaker;
        _serialDataManager.Data.TutorialUpgrade = _tutorialManager.TutorialUpgrade;
        _serialDataManager.Data.TutorialRecycling = _tutorialManager.TutorialRecycling;
        _serialDataManager.Data.TutorialFullAutotaker = _tutorialManager.TutorialFullAutotaker;
        _serialDataManager.Data.TutorialNextLevel = _tutorialManager.TutorialNextLevel;
        _serialDataManager.Data.TutorialAutotakerFirstTime = _tutorialManager.TutorialAutotakerFirstTime;

        _serialDataManager.Data.SpecialOffer = _purchaseControl.SpecialOffer;
        _serialDataManager.Data.OneTimeOffer = _purchaseControl.OneTimeOffer;
        _serialDataManager.Data.NoAdsBoost = _purchaseControl.NoAdsBoost;

        _serialDataManager.Data.JunkBotSet = _purchaseControl.JunkBotSet;
        _serialDataManager.Data.ForceBoost = _purchaseControl.ForceBoost;
        _serialDataManager.Data.CrusherBoost = _purchaseControl.CrusherBoost;
        _serialDataManager.Data.TrailerBoost = _purchaseControl.TrailerBoost;
        _serialDataManager.Data.MagnetBoost = _purchaseControl.MagnetBoost;

        //Analytics
        _analyticsTimerService.EndTimer();
        _serialDataManager.Data.AnalyticsCountPlatform = gameManager.AnalyticsCountPlatform;
        _serialDataManager.Data.AnalyticsFirsPurchase = _purchaseControl.AnalyticsFirstPurchase;

        _serialDataManager.SaveDataTrash();
    }
}
