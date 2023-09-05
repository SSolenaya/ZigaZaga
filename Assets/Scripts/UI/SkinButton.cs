using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkinButton : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Button btn;

    public void Setup(Sprite spr, Action act)
    {
        img.sprite = spr;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(()=> act?.Invoke());
    }
}
