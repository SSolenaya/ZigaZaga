using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public ClickObserver _pauseClickObserver;
    public ClickObserver _clickInPlayObserver;

    [SerializeField] private Button _soundBtn;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private TMP_Text _sessionGemsAmount;
    [SerializeField] private CanvasGroup _menuCanvasGroup;
    [SerializeField] private Text _bestScoreTxt;
    [SerializeField] private Text _gamesNumTxt;
    [SerializeField] private Text _totalScoreTxt;

    public void Init()
    {
        ShowMenu();
        SetupSoundButtonFunc();
        SetupSettingsButtonFunc();
        _pauseClickObserver.SubscribeForClick(Play);
    }

    public void SetupSoundButtonFunc()
    {
        _settingsBtn.onClick.RemoveAllListeners();
        _settingsBtn.onClick.AddListener(() => {
            AudioController.Inst.PlayClickSound();
            // TODO: settings win
        });
    }

    public void SetupSettingsButtonFunc()
    {
        _soundBtn.image.sprite = AudioController.Inst.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        _soundBtn.onClick.RemoveAllListeners();
        _soundBtn.onClick.AddListener(() => {
            AudioController.Inst.PlayClickSound();
            AudioController.Inst.SwitchSound();
            _soundBtn.image.sprite = AudioController.Inst.IsSoundOn ? _soundOnSprite : _soundOffSprite;
        });
    }

    public void ChangeSessionProgressText(int amount) //  current gems amount per session
    {
        _sessionGemsAmount.text = amount.ToString("0000");
    }

    public void ShowMenu()
    {
        _bestScoreTxt.text = $"BEST SCORE: {PlayerPrefsManager.GetBestScore()}";
        _gamesNumTxt.text = $"GAMES PLAYED: {PlayerPrefsManager.GetGamesNumber()}";
        _totalScoreTxt.text = $"TOTAL SCORE: {PlayerPrefsManager.GetTotalScore()}";
        _menuCanvasGroup.alpha = 1;
        _menuCanvasGroup.blocksRaycasts = true;
    }

    public void Play()
    {
        _menuCanvasGroup.alpha = 0;
        _menuCanvasGroup.blocksRaycasts = false;
        MainLogic.Inst.SetGameState(GameStates.play);
    }
}