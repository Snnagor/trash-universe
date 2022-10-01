using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animation))]
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private bool tutorialControl;
    [SerializeField] private bool tutorialTeleport;
    [SerializeField] private bool tutorialRecyclingAutotaker;
    [SerializeField] private bool tutorialUpgrade;
    [SerializeField] private bool tutorialRecycling;
    [SerializeField] private bool tutorialFullAutotaker;
    [SerializeField] private bool tutorialNextLevel;
    [SerializeField] private bool tutorialAutotakerFirstTime;
    [SerializeField] private GameObject tutorialCanvas;
    

    public bool TutorialControl { get => tutorialControl; set => tutorialControl = value; }
    public bool TutorialTeleport { get => tutorialTeleport; set => tutorialTeleport = value; }
    public bool TutorialRecyclingAutotaker { get => tutorialRecyclingAutotaker; set => tutorialRecyclingAutotaker = value; }
    public bool TutorialUpgrade { get => tutorialUpgrade; set => tutorialUpgrade = value; }
    public bool TutorialRecycling { get => tutorialRecycling; set => tutorialRecycling = value; }
    public bool TutorialFullAutotaker { get => tutorialFullAutotaker; set => tutorialFullAutotaker = value; }
    public bool TutorialNextLevel { get => tutorialNextLevel; set => tutorialNextLevel = value; }
    public bool TutorialAutotakerFirstTime { get => tutorialAutotakerFirstTime; set => tutorialAutotakerFirstTime = value; }

    private Animation anim;

    private bool tutorialControltUpdate;
    private bool subscribeSignalMainTrash;
    private bool subscribeSignalFirstSell;
    private bool subscribeSignalForUpgrade;
    private bool subscribeSignalForRecycling;
    
    #region Injects

    private FloatingJoystick _floatingJoystick;
    private SignalBus _signalBus;
    private CameraControl _cameraControl;
    private TrailerTrash _trailerTrash;
    private EnableButtonsBuy _enableButtonsBuy;
    private AnalyticsTutorial _analyticsTutorial;
    private GameManager _gameManager;
    private PlatformRecyclingMove _platformRecyclingMove;
    private ResourcePlace _resourcePlace;

    [Inject]
    private void Construct(FloatingJoystick floatingJoystick,
                           SignalBus signalBus,
                           CameraControl cameraControl,
                           TrailerTrash trailerTrash,
                           EnableButtonsBuy enableButtonsBuy,
                           AnalyticsTutorial analyticsTutorial,
                           GameManager gameManager,
                           PlatformRecyclingMove platformRecyclingMove,
                           ResourcePlace resourcePlace)
    {
        _floatingJoystick = floatingJoystick;
        _signalBus = signalBus;
        _cameraControl = cameraControl;
        _trailerTrash = trailerTrash;
        _enableButtonsBuy = enableButtonsBuy;
        _analyticsTutorial = analyticsTutorial;
        _gameManager = gameManager;
        _platformRecyclingMove = platformRecyclingMove;
        _resourcePlace = resourcePlace;
    }

    #endregion       

    #region Signals       

    private void OnDisable()
    {
        if (subscribeSignalMainTrash) 
        {
            _signalBus.Unsubscribe<MainTrashPlatformArrivalSignal>(StartAnimationControl);
            subscribeSignalMainTrash = false;
        }            

        if(subscribeSignalFirstSell) 
        {
            _signalBus.Unsubscribe<FirstSellSignal>(StartTutorailRecycling);
            subscribeSignalFirstSell = false;
        }

        if (subscribeSignalForUpgrade)
        {
            _signalBus.Unsubscribe<MainTrashPlatformArrivalSignal>(TutorialUpgradeSignal);
            subscribeSignalForUpgrade = false;
        }

        if (subscribeSignalForRecycling)
        {
            _signalBus.Unsubscribe<OpenDoorRecyclingSignal>(DisableTutorialRecycling);
            subscribeSignalForRecycling = false;
        }
    }

    private void StartTutorailRecycling()
    {
        if (TutorialRecyclingAutotaker)
        {
            TutorialRecyclingAutotaker = false;
            DisableTutorialTeleport();
            StartCoroutine(StartTutorialBuyButtons());
            
        }        
    }

    private void DisableTutorialTeleport()
    {       
        TutorialTeleport = false;
    }

    #endregion

    public void LocalAwake()
    {
        anim = GetComponent<Animation>();
        
        InitTutorialControl();
        InitTutorialRecyclingAutotaker();
        InitTutorialNextLevel();
    }

    private void Start()
    {
        SubscribeSignal();
    }

    private void SubscribeSignal()
    {
        if (TutorialControl)
        {
            _signalBus.Subscribe<MainTrashPlatformArrivalSignal>(StartAnimationControl);
            subscribeSignalMainTrash = true;
        }
            

        if (TutorialTeleport || TutorialRecyclingAutotaker) 
        {
            _signalBus.Subscribe<FirstSellSignal>(StartTutorailRecycling);
            subscribeSignalFirstSell = true;
        }

        if (TutorialUpgrade)
        {           
            _signalBus.Subscribe<MainTrashPlatformArrivalSignal>(TutorialUpgradeSignal);
            subscribeSignalForUpgrade = true;
        }

        if (TutorialRecycling)
        {
            _signalBus.Subscribe<OpenDoorRecyclingSignal>(DisableTutorialRecycling);
            subscribeSignalForRecycling = true;
        }            
    }

    private void DisableTutorialRecycling()
    {
        if (TutorialRecycling)
        {
            TutorialRecycling = false;
        }
    }

    private void TutorialUpgradeSignal()
    {       
        if (_gameManager.AnalyticsCountPlatform == 2 && TutorialUpgrade)
        {           
            StartCoroutine(StartTutorailUpgrade());
            TutorialUpgrade = false;
        }
    }

    private void InitTutorialRecyclingAutotaker()
    {
        if (TutorialRecyclingAutotaker)
        {
            _enableButtonsBuy.DisableButtons();
        }
        else
        {
            _enableButtonsBuy.EnableButtons();
        }
    }

    private void InitTutorialNextLevel()
    {
        if (TutorialNextLevel)
        {
            _enableButtonsBuy.DisableNextLevelButton();
        }
        else
        {
            _enableButtonsBuy.EnableNextLevelButton();
        }
    }

    private void InitTutorialControl()
    {
        if (TutorialControl)
        {
            tutorialCanvas.SetActive(true);
            _floatingJoystick.Controll = false;
        }
    }

    public void InitTutorailAutotraker()
    {
        _floatingJoystick.Controll = false;
    }

    public void StopTutorialAutotaker()
    {
        if (TutorialAutotakerFirstTime && !_floatingJoystick.Controll)
        {
            _floatingJoystick.Controll = true;            
            _cameraControl.RobotCam();
            TutorialAutotakerFirstTime = false;
        }
    }

    public void StartAnimationControl()
    {
        if (TutorialControl)
            anim.Play();
    }

    public void ChangeCameraTutorial()
    {
        _cameraControl.TutorialTrashCam();

        tutorialControltUpdate = true;
        _floatingJoystick.Controll = true;
    }

    public void ChangeCameraRobot()
    {
        _cameraControl.RobotCam();
    }

    public void StopTutorialControl()
    {      
        if (anim.isPlaying)
        {            
            anim.Stop();
            _cameraControl.RobotCam();
            tutorialCanvas.SetActive(false);            
            _floatingJoystick.Controll = true;
            tutorialControltUpdate = false;
            TutorialControl = false;

            _analyticsTutorial.SetEvent(001, "Control Tutorial");
        }        
    }

    private void Update()
    {
        if (tutorialControltUpdate)
        {           
            if (Input.GetMouseButton(0))
            {                
                StopTutorialControl();
            }
        }

        if (TutorialTeleport)
        {
            if (_trailerTrash.FullTrailer)
            {                
                StartCoroutine(StartTutorailTeleport());
                TutorialTeleport = false;
            }            
        }

        if (TutorialRecycling && _platformRecyclingMove.Built)
        {
            if (_trailerTrash.FullTrailer)
            {
                StartCoroutine(StartTutorialRecycling());
                TutorialRecycling = false;
            }
        }

        if (TutorialFullAutotaker)
        {
            if (_resourcePlace.IsTrailerFull())
            {
                StartCoroutine(StartTutorialFullAutotaker());
                TutorialFullAutotaker = false;
            }
        }

        if (TutorialNextLevel)
        {
            if (_gameManager.AnalyticsCountPlatform == 4)
            {
                StartCoroutine(StartTutorialNextLevel());
                TutorialNextLevel = false;
            }
        }

    }

    IEnumerator StartTutorailTeleport()
    {
        yield return new WaitForSeconds (2f);

        _floatingJoystick.Controll = false;
        _cameraControl.TutorialTeleportCam();

        yield return new WaitForSeconds(2f);

        _cameraControl.RobotCam();        
        _floatingJoystick.Controll = true;

        _analyticsTutorial.SetEvent(002, "Teleport Tutorial");
    }

    IEnumerator StartTutorialBuyButtons()
    {
        yield return new WaitForSeconds(2f);

        _floatingJoystick.Controll = false;
       
        _cameraControl.TutorialBuyButtonRecyclingCam();

        yield return new WaitForSeconds(1f);

        _enableButtonsBuy.RecyclingButtonBuyShow();               

        yield return new WaitForSeconds(1f);

        _cameraControl.TutorialBuyButtonAutotakerCam();

        yield return new WaitForSeconds(1f);

        _enableButtonsBuy.AutotakerButtonBuyShow();

        _analyticsTutorial.SetEvent(003, "Buy Grabber and Recycling Tutorial");

        yield return new WaitForSeconds(1f);

        _cameraControl.RobotCam();
        _floatingJoystick.Controll = true;
    }

    IEnumerator StartTutorailUpgrade()
    {
        yield return new WaitForSeconds(1f);

        _floatingJoystick.Controll = false;
        _cameraControl.TutorialUpgradeCam();

        yield return new WaitForSeconds(2f);

        _cameraControl.RobotCam();
        _floatingJoystick.Controll = true;

        _analyticsTutorial.SetEvent(004, "Upgrade Tutorial");
    }

    IEnumerator StartTutorialRecycling()
    {
        yield return new WaitForSeconds(2f);

        _floatingJoystick.Controll = false;
        _cameraControl.TutorialRecyclingCam();

        yield return new WaitForSeconds(2f);

        _cameraControl.RobotCam();
        _floatingJoystick.Controll = true;

        _analyticsTutorial.SetEvent(005, "Recycling Tutorial");
    }

    IEnumerator StartTutorialFullAutotaker()
    {
        yield return new WaitForSeconds(0.5f);

        _floatingJoystick.Controll = false;
        _cameraControl.AutotakerCam();

        yield return new WaitForSeconds(2f);

        _cameraControl.RobotCam();
        _floatingJoystick.Controll = true;

        _analyticsTutorial.SetEvent(006, "Grabber Full Tutorial");
    }

    IEnumerator StartTutorialNextLevel()
    {
        yield return new WaitForSeconds(2f);

        _floatingJoystick.Controll = false;

        _cameraControl.TutorialNextLevelCam();

        yield return new WaitForSeconds(1f); 

        _enableButtonsBuy.NextLevelButtonBuyShow();

        yield return new WaitForSeconds(1f);

        _cameraControl.RobotCam();

        _floatingJoystick.Controll = true;

        _analyticsTutorial.SetEvent(007, "Next Level Tutorial");
    }
}
