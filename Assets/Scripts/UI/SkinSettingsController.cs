using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSettingsController : MonoBehaviour
{
    [SerializeField] private SkinButton _skinButtonPrefab;
    [SerializeField] private Transform itemsParent;
    private List<Button> skinButtonList = new List<Button>();
    private MainLogic _mainLogic;
    private AudioController _audioController;
    private ScrollViewBuilder _scrollViewBuilder;
    private BallSkinData currentSkinData;

    public void Setup(MainLogic mainLogic, AudioController audioController)
    {
        _mainLogic = mainLogic;
        _audioController = audioController;
        ScrollViewBuilder _scrollViewBuilder = new ScrollViewBuilder();
        _scrollViewBuilder.Build<BallSkinData>(_mainLogic.BallMaterialsManagerSO.skinsList, _skinButtonPrefab, itemsParent, ChoseBallSkin);
    }

    private void ChoseBallSkin(IBaseScrollViewItemData newSkinData)
    {
        if (newSkinData is not BallSkinData ballSkinData || ballSkinData == currentSkinData) return;
        _audioController.PlayClickSound();
        _mainLogic.ChangeBallSkin(newSkinData);
    }
}
