using Zenject;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UIWindowsPrefabInstaller", menuName = "ScriptableObject/UIWindowsPrefabInstaller", order = 3)]
public class UIWindowsPrefabInstaller : ScriptableObjectInstaller
{
    [SerializeField] private UIWindowsPrefabsHolder _uiWinsPrefabHolder;
    [SerializeField] private GameCanvas _gameCanvasPrefab;
    [SerializeField] private UIWindowsManager _uIWindowsManagerPrefab;
    [SerializeField] private MainLogic _mainLogicPrefab;
    [SerializeField] private AudioController _audioControllerPrefab;
    [SerializeField] private BallController _ballController;
    [SerializeField] private RoadController _roadController;
    


    public override void InstallBindings()
    {
        Container.Bind<UIWindowsPrefabsHolder>().FromInstance(_uiWinsPrefabHolder).NonLazy();

        Container.Bind<GameCanvas>()
                .FromComponentInNewPrefab(_gameCanvasPrefab)
                .AsSingle()
                .NonLazy();

        Container.Bind<UIWindowsManager>()
                 .FromComponentInNewPrefab(_uIWindowsManagerPrefab)
                 .AsSingle()
                 .NonLazy();

        Container.Bind<MainLogic>()
                .FromComponentInNewPrefab(_mainLogicPrefab)
                .AsSingle()
                .NonLazy();

        Container.Bind<AudioController>()
                .FromComponentInNewPrefab(_audioControllerPrefab)
                .AsSingle()
                .NonLazy();

        Container.Bind<BallController>()
                .FromComponentInNewPrefab(_ballController)
                .AsSingle()
                .NonLazy();

        Container.Bind<RoadController>()
                .FromComponentInNewPrefab(_roadController)
                .AsSingle()
                .NonLazy();

    }
}

