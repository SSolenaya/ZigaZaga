using UnityEngine;
using Zenject;

public class ControllersPrefabInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
       //Container.Bind<GameCanvas>()
       //        .FromComponentInNewPrefab(_gameCanvas)
       //        .AsSingle()
       //        .NonLazy();
    }
}