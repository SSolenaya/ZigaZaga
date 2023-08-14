using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverWindow : BaseUiWindow
{
    [SerializeField] private Text _currentScoreTxt;
    [SerializeField] private Text _bestScoreTxt;

    public void Start()
    {
        fullScreenClickObserver.SubscribeForClick(() => {
            _mainLogic.SetGameState(GameStates.readyToPlay);
        });
    }

    public void OnEnable()
    {
        _currentScoreTxt.text = StringConfig.currentScore + " " + _gameInfoManager.CurrentScore;
        _bestScoreTxt.text = StringConfig.bestScore + " " + SaveManager.GetBestScore();
    }
}