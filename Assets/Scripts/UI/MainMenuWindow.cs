using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        SetupSoundButtonFunc();
        SetupSettingsButtonFunc();
        fullScreenClickObserver.SubscribeForClick(() => {
            _mainLogic.SetGameState(GameStates.play);
            SaveManager.SetPlayedGamesCount();
        });
    }

    public void OnEnable()
    {
        _bestScoreTxt.text = StringConfig.bestScore + " " + SaveManager.GetBestScore();
        _gamesCountTxt.text = StringConfig.gamesPlayed + " " + SaveManager.GetPlayedGamesCount();
        _totalGemsTxt.text = StringConfig.totalGems + " " + SaveManager.GetTotalGemsCount();
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
        _soundBtn.image.sprite = _audioController.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        _soundBtn.onClick.RemoveAllListeners();
        _soundBtn.onClick.AddListener(() => {
            _audioController.PlayClickSound();
            _audioController.SwitchSound();
            _soundBtn.image.sprite = _audioController.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        });
    }
}