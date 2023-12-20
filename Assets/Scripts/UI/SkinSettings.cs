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
    private MainLogic _mainLogic;

    public void Setup(MainLogic mainLogic)
    {
        _mainLogic = mainLogic;

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
            _mainLogic.ChangeBallSkin(skinData);
        });
    }
}
