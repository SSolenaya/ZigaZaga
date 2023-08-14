using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PoolManager>().AsSingle().NonLazy();

        Container.Bind<GameInfoManager>().AsSingle().NonLazy();
    }
}