using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(UIController))]
public class BtnContoller : MonoBehaviour
{
    [SerializeField] private GameObject iconNoAdsOnShopButton;

    private UIController uiController;

    #region Injects    

    private SoundManager _soundManager;
    private SignalBus _signalBus;
    private SerialDataManager _serialDataManager;
    private PurchaseControl _purchaseControl;
    private LoaderDataGame _loaderDataGame;

    [Inject]
    private void Construct(SoundManager soundManager,
                           SignalBus signalBus,
                           SerialDataManager serialDataManager,
                           PurchaseControl purchaseControl,
                           LoaderDataGame loaderDataGame)
    {
        _soundManager = soundManager;
        _signalBus = signalBus;
        _serialDataManager = serialDataManager;
        _purchaseControl = purchaseControl;
        _loaderDataGame = loaderDataGame;
    }

    #endregion

    private void Awake()
    {
        uiController = GetComponent<UIController>();
    }

    private void Start()
    {
        DisableIconNoAds();
    }

    public void DisableIconNoAds()
    {        
        if (iconNoAdsOnShopButton)
            iconNoAdsOnShopButton.SetActive(!_purchaseControl.InterstitialAd);
    }

    public void RestartBtn()
    {
        _soundManager.Click();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextBtn()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            _soundManager.Click();
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void PauseBtn()
    {
        _soundManager.Click();
        uiController.PausePanel();
    }

    public void ShopBtn()
    {
        _soundManager.Click();
        uiController.ShopPanel();
    }

    public void ResumeBtn()
    {
        uiController.GamePanel();
        _soundManager.Click();
    }

    public void MainMenuBtn()
    {
        _soundManager.Click();
        SceneManager.LoadScene(0);
    }

    public void QuitBtn()
    {
        _soundManager.Click();
        Application.Quit();
    }

    public void MusicBtn()
    {
        _soundManager.Click();
        _signalBus.Fire(new MusicSignal());

    }

    public void SoundBtn()
    {
        _soundManager.Click();
        _signalBus.Fire(new SoundSignal());
    }

    public void VibroBtn()
    {
        _soundManager.Click();
        _signalBus.Fire(new VibroSignal());
    }

    public void TestCleanAllSaveBtn()
    {
        _serialDataManager.ResetData();
        SceneManager.LoadScene(1);
    }


    public void RestertLevel()
    {
        _loaderDataGame.SaveAllData();
        _serialDataManager.RestartNewLevel();
        RestartBtn();
    }

}
