using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : BaseUiWindow
{
    [SerializeField] private SoundButtonController _soundButtonController;
    [SerializeField] private StatisticsController _statisticsController;
    [SerializeField] private GameModeController _gameModeController;
    [SerializeField] private SkinSettingsController _skinSettingsController;

    public override void SetupInnerElements()
    {
        _soundButtonController.Setup(_audioController);
        _gameModeController.Setup(_mainLogic, _audioController);
        _skinSettingsController.Setup(_mainLogic, _audioController);
        fullScreenClickObserver.SubscribeForClick(() => {
            _mainLogic.SetGameState(GameStates.play);
            SaveManager.SetPlayedGamesCount();
        });
    }

    public void OnEnable()
    {
        _statisticsController.ShowActualData();
    }

    
}