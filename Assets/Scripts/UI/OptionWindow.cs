using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : BaseUiWindow
{
    public Toggle cheatToggle;

    public void Start()
    {
        fullScreenClickObserver.SubscribeForClick(() => {
            WindowManager.Inst.OpenWindow(TypeWindow.mainMenu);
        });

        cheatToggle.onValueChanged.AddListener((var) => {
            MainLogic.Inst.SetCheatMode(var);
        });
    }

    public void OnEnable()
    {
        cheatToggle.isOn = MainLogic.Inst.GetCheatModeState();
    }

}
