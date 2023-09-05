using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuWindow : BaseUiWindow
{
    
    [SerializeField] private TMP_Text _bestScoreTxt;
    [SerializeField] private TMP_Text _gamesCountTxt;
    [SerializeField] private TMP_Text _totalGemsTxt;

    [SerializeField] private Button _soundBtn;
    [SerializeField] private Image _soundBtnImg;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Button _settingsBtn;


    public void Start()
    {
        SetupSoundButtonFunc();
        SetupSettingsButtonFunc();
        fullScreenClickObserver.SubscribeForClick(() => {
            _mainLogic.SetGameState(GameStates.play);
            SaveManager.SetPlayedGamesCount();
        });
    }

    public void OnEnable()
    {
        _bestScoreTxt.text = SaveManager.GetBestScore().ToString();
        _gamesCountTxt.text = SaveManager.GetPlayedGamesCount().ToString();
        _totalGemsTxt.text = SaveManager.GetTotalGemsCount().ToString();
    }

    public void SetupSettingsButtonFunc()
    {
        _settingsBtn.onClick.RemoveAllListeners();
        _settingsBtn.onClick.AddListener(() => {
            _audioController.PlayClickSound();
            _windowsManager.OpenWindow(TypeWindow.options);
        });
    }

    public void SetupSoundButtonFunc()
    {
        _soundBtnImg.sprite = _audioController.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        _soundBtn.onClick.RemoveAllListeners();
        _soundBtn.onClick.AddListener(() => {
            _audioController.PlayClickSound();
            _audioController.SwitchSound();
            _soundBtnImg.sprite = _audioController.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        });
    }
}