
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "UIWindowsPrefabsHolder", menuName = "ScriptableObject/UIWindowsPrefabsHolder", order = 2)]
    public class UIWindowsPrefabsHolder : ScriptableObject
    {
        public List<BaseUiWindow> listUIWindowsPrefabs;

        public BaseUiWindow GetWinByType(TypeWindow typeWindow)
        {
            foreach (BaseUiWindow baseUiWindow in listUIWindowsPrefabs)
            {
                if (baseUiWindow.typeWindow == typeWindow)
                {
                    return baseUiWindow;
                }
            }

            return null;
        }

    }
