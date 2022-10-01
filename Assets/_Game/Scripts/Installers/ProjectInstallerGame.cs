using UnityEngine;
using Zenject;

public class ProjectInstallerGame : MonoInstaller
{    
    public override void InstallBindings()
    {
        SignalBind();
    }

    private void SignalBind()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<MusicSignal>();
        Container.DeclareSignal<SoundSignal>();
        Container.DeclareSignal<VibroSignal>();
        Container.DeclareSignal<ScoreSignal>();
        Container.DeclareSignal<CreatePackedTrashSignal>();
        Container.DeclareSignal<TookPackedTrashSignal>();
        Container.DeclareSignal<NewPlatformSignal>();
        Container.DeclareSignal<ScoreResourcesSignal>();
        Container.DeclareSignal<StartRecyclingSignal>();
        Container.DeclareSignal<OpenDoorRecyclingSignal>();
        Container.DeclareSignal<CloseDoorRecyclingSignal>();
        Container.DeclareSignal<FindingPlatformSignal>();
        Container.DeclareSignal<BonusCrusherSignal>();
        Container.DeclareSignal<BonusOpenUISignal>();
        Container.DeclareSignal<MainTrashPlatformArrivalSignal>();
        Container.DeclareSignal<FirstSellSignal>();
    }
}