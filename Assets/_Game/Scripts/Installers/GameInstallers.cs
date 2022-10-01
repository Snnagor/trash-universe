using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstallers : MonoInstaller
{
    [Inject]
    Settings _settings;

    public override void InstallBindings()
    {
        BindFactory();

        Container.Bind<SerialDataManager>().AsSingle().NonLazy();
        Container.Bind<PurchaseControl>().AsSingle().NonLazy();
    }

    private void BindFactory()
    {
        Container.BindFactory<AudioSource, BackgroundMusicFactory>()
              .FromComponentInNewPrefab(_settings.BackgroundMusicPrefab)
              .WithGameObjectName("BackgroundMusic")
              .UnderTransformGroup("GameManager");
    }

}