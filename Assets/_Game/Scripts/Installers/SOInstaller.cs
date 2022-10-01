using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private Settings _settings;
    [SerializeField] private SettingsShop _settingsShop;

    public override void InstallBindings()
    {
        Container.Bind<Settings>().FromInstance(_settings).AsSingle().NonLazy();
        Container.Bind<SettingsShop>().FromInstance(_settingsShop).AsSingle().NonLazy();
    }
}
