using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : BaseUiWindow
{
    [SerializeField] private Text _currentScoreTxt;
    [SerializeField] private Text _bestScoreTxt;

    public void Start()
    {
        _currentScoreTxt.text = 10.ToString(); //$"BEST SCORE: {SaveManager.GetBestScore()}";
        _bestScoreTxt.text = 11.ToString(); //$"GAMES PLAYED: {SaveManager.GetGamesNumber()}";

        fullScreenClickObserver.SubscribeForClick(() => {
            MainLogic.Inst.SetGameState(GameStates.readyToPlay);
        });
    }
}