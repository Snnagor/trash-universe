using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roborCamera;
    [SerializeField] private CinemachineVirtualCamera recyclingCamera;
    [SerializeField] private CinemachineVirtualCamera autotakerCamera;
    [SerializeField] private CinemachineVirtualCamera upgradeCamera; 
    [SerializeField] private CinemachineVirtualCamera tutorialTrashCamera; 
    [SerializeField] private CinemachineVirtualCamera tutorialCamera;


    [Header("Tutorial Target")]
    [SerializeField] private Transform teleport;
    [SerializeField] private Transform buyButtonRecycling;
    [SerializeField] private Transform buyButtonAutotaker;
    [SerializeField] private Transform upgrade;
    [SerializeField] private Transform buyButtonNextLevel;

    private CinemachineVirtualCamera currentCamera;

    private void Start()
    {
        RobotCam();
    }
   
    private void ChangeCamera(CinemachineVirtualCamera state)
    {
        if (currentCamera != null)
        {
            currentCamera.Priority = 10;
        }

        currentCamera = state;

        if (currentCamera != null)
            currentCamera.Priority = 11;
    }

    public void RobotCam()
    {       
        ChangeCamera(roborCamera);
    }

    public void RecyclingCam()
    {
        if (currentCamera != recyclingCamera)
            ChangeCamera(recyclingCamera);
    }

    public void AutotakerCam()
    {
        if (currentCamera != autotakerCamera)
            ChangeCamera(autotakerCamera);
    }

    public void UpgradeCam()
    {
        if (currentCamera != upgradeCamera)
            ChangeCamera(upgradeCamera);
    }

    public void TutorialTrashCam()
    {
        if (currentCamera != upgradeCamera)
            ChangeCamera(tutorialTrashCamera);
    }

    public void TutorialTeleportCam()
    {
        tutorialCamera.Follow = teleport;

        if (currentCamera != tutorialCamera)
            ChangeCamera(tutorialCamera);
    }

    public void TutorialBuyButtonRecyclingCam()
    {
        tutorialCamera.Follow = buyButtonRecycling;

        if (currentCamera != tutorialCamera)
            ChangeCamera(tutorialCamera);
    }

    public void TutorialBuyButtonAutotakerCam()
    {
        tutorialCamera.Follow = buyButtonAutotaker;

        if (currentCamera != tutorialCamera)
            ChangeCamera(tutorialCamera);
    }

    public void TutorialUpgradeCam()
    {
        tutorialCamera.Follow = upgrade;

        if (currentCamera != tutorialCamera)
            ChangeCamera(tutorialCamera);
    }

    public void TutorialRecyclingCam()
    {
        RecyclingCam();
    }

    public void TutorialNextLevelCam()
    {
        tutorialCamera.Follow = buyButtonNextLevel;

        if (currentCamera != tutorialCamera)
            ChangeCamera(tutorialCamera);
    }
}
