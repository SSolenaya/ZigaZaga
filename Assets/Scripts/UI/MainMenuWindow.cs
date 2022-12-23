using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : BaseUiWindow
{
    [SerializeField] private Text _bestScoreTxt;
    [SerializeField] private Text _gamesCountTxt;
    [SerializeField] private Text _totalGemsTxt;

    [SerializeField] private Button _soundBtn;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Button _settingsBtn;

    public void Start()
    {
        _bestScoreTxt.text = 10.ToString();//$"BEST SCORE: {SaveManager.GetBestScore()}";
        _gamesCountTxt.text = 11.ToString();//$"GAMES PLAYED: {SaveManager.GetGamesNumber()}";
        _totalGemsTxt.text = 12.ToString();///$"TOTAL SCORE: {SaveManager.GetTotalScore()}";
        SetupSoundButtonFunc();
        SetupSettingsButtonFunc();
        fullScreenClickObserver.SubscribeForClick(() => {
           MainLogic.Inst.SetGameState(GameStates.play);
           SaveManager.SetPlayedGamesCount();
        });
    }



    public void SetupSettingsButtonFunc()
    {
        _settingsBtn.onClick.RemoveAllListeners();
        _settingsBtn.onClick.AddListener(() => {
            AudioController.Inst.PlayClickSound();
            WindowManager.Inst.OpenWindow(TypeWindow.options);
        });
    }

    public void SetupSoundButtonFunc()
    {
        _soundBtn.image.sprite = AudioController.Inst.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        _soundBtn.onClick.RemoveAllListeners();
        _soundBtn.onClick.AddListener(() => {
            AudioController.Inst.PlayClickSound();
            AudioController.Inst.SwitchSound();
            _soundBtn.image.sprite = AudioController.Inst.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        });
    }
}
