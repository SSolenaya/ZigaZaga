using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseWindow : BaseUiWindow
{
    public CheatToggle cheatToggle;

    private void Awake()
    {
        cheatToggle.Setup(_mainLogic);
        fullScreenClickObserver.SubscribeForClick(() => {
            _mainLogic.SetGameState(GameStates.play);
        });
    }

    private void OnEnable()
    {
        cheatToggle.SetActualToggleState();
    }
}
