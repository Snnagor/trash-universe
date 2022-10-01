using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartController : MonoBehaviour
{
    #region Injects

    private SerialDataManager _serialDataManager;
    private LoaderDataGame _loaderDataGame;
    private SoundManager _soundManager;
    private UIUpgradeWheels _uiUpgradeWheels;
    private UpgradeWheels _upgradeWheels;
    private PlayerMove _playerMove;
    private MainCameraBrain _mainCameraBrain;
    private ClothesManager _clothesManager;
    private TutorialManager _tutorialManager;

    [Inject]
    private void Construct(SerialDataManager serialDataManager,
                           LoaderDataGame loaderDataGame,
                           SoundManager soundManager,
                           UIUpgradeWheels uiUpgradeWheels,
                           PlayerMove playerMove,
                           MainCameraBrain mainCameraBrain,
                           ClothesManager clothesManager,
                           UpgradeWheels upgradeWheels,
                           TutorialManager tutorialManager)
    {
        _serialDataManager = serialDataManager;
        _loaderDataGame = loaderDataGame;
        _soundManager = soundManager;
        _uiUpgradeWheels = uiUpgradeWheels;
        _playerMove = playerMove;
        _mainCameraBrain = mainCameraBrain;
        _clothesManager = clothesManager;
        _upgradeWheels = upgradeWheels;
        _tutorialManager = tutorialManager;
    }

    #endregion

    private void Awake()
    {
        _upgradeWheels.LocalAwake();
        _serialDataManager.LoadGame();        
        _loaderDataGame.InitGameData();
        _playerMove.Init();
        _mainCameraBrain.Init();
        _uiUpgradeWheels.Init();
        _clothesManager.LocalAwake();
        _loaderDataGame.PurchaseLoad();
        _tutorialManager.LocalAwake();
    }


    void Start()
    {
        _soundManager.StartLocal();
        _loaderDataGame.StartLocal();
    }
}
