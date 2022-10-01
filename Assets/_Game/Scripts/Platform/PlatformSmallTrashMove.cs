using System.Collections;
using Zenject;

public class PlatformSmallTrashMove : PlatformTrashMove
{
    private AutotakerTrashControl autotakerTrashControl;
    public bool IsTherePlatform { get; set; }

    #region Injects

    private AutotakerAnim _autotakerAnim;

    [Inject]
    private void Construct(AutotakerAnim autotakerControl)
    {
        _autotakerAnim = autotakerControl;
    }

    #endregion

    private void Awake()
    {
        autotakerTrashControl = GetComponent<AutotakerTrashControl>();
    }

    public override void NewTrash()
    {        
        autotakerTrashControl.NewTrashSignal();
    }

    public override void CallBackArrival()
    {
        IsTherePlatform = true;

        _autotakerAnim.OnWork();
        autotakerTrashControl.OnTakeTrash();
        
    } 

    public override void CallBackDeparture()
    {
        IsTherePlatform = false;
    }

    public override void CallBackStartArrival()
    {        
        autotakerTrashControl.NewTrashSignal();
    }
}
