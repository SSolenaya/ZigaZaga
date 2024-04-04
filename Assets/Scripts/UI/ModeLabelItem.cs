using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ModeLabelItem : ScrollViewItem
{
    [SerializeField] private TMP_Text _modeLabelTxt;
    [SerializeField] private Button _btn;
    [SerializeField] private Image _frame;
    private GameModeData _modeData;

    public override void Configurate(IBaseScrollViewItemData data)
    {
        _modeData = (GameModeData)data;
        _modeLabelTxt.text = _modeData.mode.ToString();
    }

    public override void SetItemChecked(bool isChecked)
    {
        _frame.gameObject.SetActive(isChecked);
    }

    public override void SetupOnClickAction(Action<IBaseScrollViewItemData> act)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(() => act?.Invoke(_modeData));
    }
}

