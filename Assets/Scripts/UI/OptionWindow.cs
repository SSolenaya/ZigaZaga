using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionWindow : BaseUiWindow
{
    [SerializeField] private CheatToggle cheatToggle;
    [SerializeField] private SpeedSettingsAdjust _speedSettingsAdjust;

    public void Awake()
    {
        cheatToggle.Setup(_mainLogic);
        _speedSettingsAdjust.Setup(_mainLogic);
        fullScreenClickObserver.SubscribeForClick(() => {
            _windowsManager.OpenWindow(TypeWindow.mainMenu);
        });
    }

    private void OnEnable()
    {
        cheatToggle.SetActualToggleState();
        _speedSettingsAdjust.SetActualSliderState();
    }
}
