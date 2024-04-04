using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;


public enum TypeWindow
{
    mainMenu,
    inGame,
    gameOver,
    pause
}
public class BaseUiWindow : MonoBehaviour
{
    public TypeWindow typeWindow;
    public ClickObserver fullScreenClickObserver;
    protected MainLogic _mainLogic;
    protected AudioController _audioController;
    protected UIWindowsManager _windowsManager;
    protected GameInfoManager _gameInfoManager;

    [SerializeField] private bool isOpen;


    public void Open()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        gameObject.SetActive(true);
    }


    public void Close()
    {
        if (!isOpen)
        {
            return;
        }

        isOpen = false;
        gameObject.SetActive(false);
    }

    public void Setup(MainLogic mainLogic, AudioController audioController, UIWindowsManager windowsManager, GameInfoManager gameInfoManager)
    {
        _mainLogic = mainLogic;
        _audioController = audioController;
        _windowsManager = windowsManager;
        _gameInfoManager = gameInfoManager;
        SetupInnerElements();
    }

    public virtual void SetupInnerElements(){ }

}
