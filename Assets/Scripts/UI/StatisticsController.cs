using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsController : MonoBehaviour
{
    [SerializeField] private TMP_Text _bestScoreTxt;
    [SerializeField] private TMP_Text _gamesCountTxt;
    [SerializeField] private TMP_Text _totalGemsTxt;

    public void ShowActualData()
    {
        _bestScoreTxt.text = SaveManager.GetBestScore().ToString();
        _gamesCountTxt.text = SaveManager.GetPlayedGamesCount().ToString();
        _totalGemsTxt.text = SaveManager.GetTotalGemsCount().ToString();
    }
}
