using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : BaseUiWindow
{
    private void Start()
    {
        fullScreenClickObserver.SubscribeForClick(() => {
            MainLogic.Inst.SetGameState(GameStates.play);
        });
    }
}
