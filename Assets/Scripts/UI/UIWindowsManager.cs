using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UIWindowsManager:MonoBehaviour
{
    [Inject] private MainLogic _mainLogic;
    [Inject] private AudioController _audioController;
    [Inject] private GameInfoManager _gameInfoManager;
    [Inject] private GameCanvas gameCanvas;
    [Inject] private UIWindowsPrefabsHolder _prefabHolder;
    [Inject] private DiContainer _diContainer;
    [SerializeField] private List<BaseUiWindow> _listWindows = new List<BaseUiWindow>();
    private Transform _parent;

    [Inject]
    private void Setup()
    {
        _parent = gameCanvas.parentForUIWindows;
        CreateUIWindows();
    }

    private void Start()
    {
        OpenWindow(TypeWindow.mainMenu);
    }

    private void CreateUIWindows()
    {
        foreach (var UIWin in _prefabHolder.listUIWindowsPrefabs)
        {
                var newUIWin = GameObject.Instantiate(UIWin, _parent);
                newUIWin.Setup(_mainLogic, _audioController,this, _gameInfoManager);
                _diContainer.Inject(newUIWin);
                _listWindows.Add(newUIWin);
        }
    }


    public void OpenWindow(TypeWindow typeWindow)
    {
        foreach (BaseUiWindow baseUiWindow in _listWindows)
        {
            if (baseUiWindow.typeWindow == typeWindow)
            {
                baseUiWindow.Open();
            }
            else
            {
                baseUiWindow.Close();
            }
        }
    }


    public void CloseWindow(TypeWindow typeWindow)
    {
        foreach (BaseUiWindow baseUiWindow in _listWindows)
        {
            if (baseUiWindow.typeWindow == typeWindow)
            {
                baseUiWindow.Close();
            }
        }
    }

    public T GetWindow<T>(TypeWindow typeWindow) where T : BaseUiWindow
    {
        foreach (BaseUiWindow baseUiWindow in _listWindows)
        {
            if (baseUiWindow.typeWindow == typeWindow)
            {
                return (T) baseUiWindow;
            }
        }

        return null;
    }
}