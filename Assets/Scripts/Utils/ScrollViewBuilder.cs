using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewBuilder
{
    private List<ScrollViewItem> itemsList = new List<ScrollViewItem> ();
    private ScrollViewItem currentPickedItem;

    public void Build<T>(List<T> dataList, ScrollViewItem itemPrefab, Transform itemsParent, Action<IBaseScrollViewItemData> onItemCLick) where T : IBaseScrollViewItemData
    {
        if (dataList == null || dataList.Count == 0)
        {
            Debug.LogError("There is no game modes data");
            return;
        }
        foreach (var itemData in dataList)
        {
            ScrollViewItem item = GameObject.Instantiate(itemPrefab, itemsParent);
            item.Configurate(itemData);
            itemsList.Add(item);
            item.SetupOnClickAction((data) => {
                this.PickOneItem(item);
                onItemCLick?.Invoke(data);
             });
        }
        currentPickedItem = itemsList[0];
        currentPickedItem.SetItemChecked(true);
    }

    private void PickOneItem(ScrollViewItem pickedItem)
    {
        if (currentPickedItem == pickedItem) return;
        currentPickedItem = pickedItem;
        foreach (var item in itemsList)
        {
            item.SetItemChecked(false);
        }
        currentPickedItem.SetItemChecked(true);
    }

}
