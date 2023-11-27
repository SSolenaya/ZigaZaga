using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSettings : MonoBehaviour
{
    [SerializeField] private SkinButton _skinButtonPrefab;
    [SerializeField] private Transform _parentForSkinButtons;
    private List<Button> skinButtonList = new List<Button>();
    private Action <BallSkinData> onSkinChangeAct;

    public void Setup(MainLogic mainLogic)
    {
        mainLogic.ChangeBallSkin(onSkinChangeAct);

        foreach (var skin in mainLogic.BallMaterialsManagerSO.skinsList)
        {
            CreateSkinButton(skin);
        }
        
    }

    private void CreateSkinButton(BallSkinData skinData)
    {
        var btn = Instantiate(_skinButtonPrefab, _parentForSkinButtons);
        btn.Setup(skinData.sprite, () =>
        {
            onSkinChangeAct?.Invoke(skinData);
        });
    }
}
