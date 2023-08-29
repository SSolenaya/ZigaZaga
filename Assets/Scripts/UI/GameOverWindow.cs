using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverWindow : BaseUiWindow
{
    [SerializeField] private TMP_Text _currentScoreTxt;
    [SerializeField] private TMP_Text _bestScoreTxt;

    public void Start()
    {
        fullScreenClickObserver.SubscribeForClick(() => {
            _mainLogic.SetGameState(GameStates.readyToPlay);
        });
    }

    public void OnEnable()
    {
        _currentScoreTxt.text = _gameInfoManager.CurrentScore.ToString();
        _bestScoreTxt.text = SaveManager.GetBestScore().ToString();
    }
}