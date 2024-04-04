using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
   [SerializeField] private ModeLabelItem itemPrefab;
   [SerializeField] private Transform itemsParent;
   private MainLogic _mainLogic;
   private AudioController _audioController;
   private ScrollViewBuilder _scrollViewBuilder;
   private GameModeData currentModeData;

    public void Setup(MainLogic mainLogic, AudioController audioController)
    {
        _mainLogic = mainLogic;
        _audioController = audioController;
        ScrollViewBuilder _scrollViewBuilder = new ScrollViewBuilder();
        _scrollViewBuilder.Build<GameModeData>(_mainLogic.GameSettingsSO.gameModes, itemPrefab, itemsParent, (_) => ChoseGameMode(_));
        ChoseGameMode(_mainLogic.GameSettingsSO.gameModes[0]);
    }

    private void ChoseGameMode(IBaseScrollViewItemData newData)
    {
        GameModeData newModeData;
        try
        {
            newModeData = (GameModeData)newData;
        } catch
        {
            Debug.LogError("Cannot cast IBaseScrollViewItemData to GameModeData in GameModeController");
            return;
        }
        if (newModeData == currentModeData) return;
        _audioController.PlayClickSound();
        currentModeData = newModeData;
        _mainLogic.SetBallSettings(currentModeData.ballSpeed);
        _mainLogic?.SetCheatMode(currentModeData.mode.CompareTo(GameModes.cheat) == 0);
    }
}


public enum GameModes
{
    easy,
    medium,
    hard,
    cheat
}
