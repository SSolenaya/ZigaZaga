using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : BaseUiWindow
{
    [SerializeField] private Text _currentScoreTxt;
    [SerializeField] private Text _bestScoreTxt;

    public void Start()
    {
        fullScreenClickObserver.SubscribeForClick(() => {
            MainLogic.Inst.SetGameState(GameStates.readyToPlay);
        });
    }

    public void OnEnable()
    {
        _currentScoreTxt.text = StringConfig.currentScore + " " + GameInfoManager.Inst.CurrentScore;
        _bestScoreTxt.text = StringConfig.bestScore + " " + SaveManager.GetBestScore();
    }
}