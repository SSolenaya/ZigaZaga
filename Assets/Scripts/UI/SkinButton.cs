using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkinButton : ScrollViewItem
{
    [SerializeField] private Image _img;
    [SerializeField] private Image _frame;
    [SerializeField] private Button _btn;
    private BallSkinData _skinData;

    public override void Configurate(IBaseScrollViewItemData data)
    {
        _skinData = (BallSkinData)data;
        _img.sprite = _skinData.sprite;
    }

    public override void SetItemChecked(bool isChecked)
    {
        _frame.gameObject.SetActive(isChecked);
    }

    public override void SetupOnClickAction(Action<IBaseScrollViewItemData> act)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(()=> act?.Invoke(_skinData));
    }
}
