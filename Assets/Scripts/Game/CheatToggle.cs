using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]

public class CheatToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    void Awake()
    {
        if (_toggle == null)
        {
            _toggle = gameObject.GetComponent<Toggle>();
        }
    }

    void OnEnable()
    {
        _toggle.isOn = MainLogic.Inst.GetCheatModeState();
    }

    void Start()
    {
        _toggle.onValueChanged.AddListener((var) => {
            MainLogic.Inst.SetCheatMode(var);
        });
    }
}
