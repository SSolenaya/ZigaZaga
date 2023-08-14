using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InGameWindow : BaseUiWindow
{
    
    [SerializeField] private TMP_Text _currentScoreTxt;
    [SerializeField] private Button pauseBtn;

    public void Start()
    {
        pauseBtn.onClick.AddListener(() => {
            _mainLogic.SetGameState(GameStates.pause);
        });

        _gameInfoManager.SubscribeForScore(SetCurrentScoreText);
    }

    private void SetCurrentScoreText(int newScore)
    {
        _currentScoreTxt.text = newScore.ToString();
    }
}