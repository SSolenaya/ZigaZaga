using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrollViewItem : MonoBehaviour
{
    public abstract void Configurate(IBaseScrollViewItemData data);
    public abstract void SetupOnClickAction(Action<IBaseScrollViewItemData> act);
    public abstract void SetItemChecked(bool isChecked);
}



