using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameWindow : BaseUiWindow
{
    [SerializeField] private TMP_Text _currentScoreTxt;
    [SerializeField] private Button pauseBtn;

    public void Start()
    {
        pauseBtn.onClick.AddListener(() => {
            MainLogic.Inst.SetGameState(GameStates.pause);
        });
    }
}