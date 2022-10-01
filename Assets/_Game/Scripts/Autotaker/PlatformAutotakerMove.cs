using UnityEngine;
using Zenject;

public class PlatformAutotakerMove : PlatformMoveHorizontal
{
    [SerializeField] private PlatformTrashMove smallTrashPlatform;

    #region Injects

    private TutorialManager _tutorialManager;

    [Inject]
    private void Construct(TutorialManager tutorialManager)
    {
        _tutorialManager = tutorialManager;      
    }

    #endregion       

    public override void CallBackArrival()
    {
        base.CallBackArrival();
        smallTrashPlatform.Arrival();        
    }

    public override void CallBackStartArrival()
    {
        _tutorialManager.InitTutorailAutotraker();
    }

}
