using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;


public enum TypeWindow
{
    mainMenu,
    inGame,
    gameOver,
    pause,
    options
}
public class BaseUiWindow : MonoBehaviour
{
    public TypeWindow typeWindow;
    public ClickObserver fullScreenClickObserver;

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

}
