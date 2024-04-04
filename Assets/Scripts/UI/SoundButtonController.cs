using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonController : MonoBehaviour
{
    [SerializeField] private Button _soundBtn;
    [SerializeField] private Image _soundBtnImg;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    public void Setup(AudioController audioController)
    {
        _soundBtnImg.sprite = audioController.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        _soundBtn.onClick.RemoveAllListeners();
        _soundBtn.onClick.AddListener(() => {
            audioController.PlayClickSound();
            audioController.SwitchSound();
            _soundBtnImg.sprite = audioController.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        });
    }
}
