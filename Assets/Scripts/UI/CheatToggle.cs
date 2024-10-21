using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Toggle))]

public class CheatToggle : MonoBehaviour
{
    private MainLogic _mainLogic;
    [SerializeField] private Toggle _toggle;

    void Awake()
    {
        if (_toggle == null)
        {
            _toggle = gameObject.GetComponent<Toggle>();
        }
    }


    public void Setup(MainLogic mainLogic)
    {
        _mainLogic = mainLogic;
        _toggle.onValueChanged.AddListener((var) => {
            _mainLogic?.SetCheatMode(var);
        });
        
    }

    public void SetActualToggleState()
    {
        _toggle.isOn = _mainLogic == null ? false : _mainLogic.GetCheatModeState();
    }
}
