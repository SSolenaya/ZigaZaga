using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpeedSettingsAdjust : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _speedSlider;
    [SerializeField] private TMP_Text _speedValue;
    [SerializeField] private TMP_Text _scoreValue;
    private MainLogic _mainLogic;


    public void Setup(MainLogic mainLogic)
    {
        _mainLogic = mainLogic;
        _speedSlider.minValue = 1;
        _speedSlider.maxValue = _mainLogic.GameSettingsSO.ballMaxSpeed;
        _speedSlider.wholeNumbers = true;
        _speedSlider.onValueChanged.AddListener(ChangeSettings);
    }

    private void ChangeSettings(float newValue)
    {
        var newSpeed = newValue;
        var newScore = _mainLogic.GameSettingsSO.scoreForGemModifier * newSpeed;
        _speedValue.text = newSpeed.ToString();
        _scoreValue.text = newScore.ToString();
        _mainLogic.SetupBallSettings(newSpeed);
    }

    public void SetActualSliderState()
    {
        _speedSlider.value = _mainLogic.GetCurrentBallSpeed();
    }

}
