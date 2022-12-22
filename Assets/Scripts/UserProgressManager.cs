using UnityEngine;

public class UserProgressManager : Singleton<UserProgressManager>
{
    private int _gemsOfThisSession; //  TODO: property with event


    public void AddOneGemToCurrentProgress()
    {
        _gemsOfThisSession += 1;
        UIController.Inst.ChangeSessionProgressText(_gemsOfThisSession);
    }

    public void Clear()
    {
        _gemsOfThisSession = 0;
        UIController.Inst.ChangeSessionProgressText(_gemsOfThisSession);
    }

    public void SaveCurrentProgress()
    {
        PlayerPrefsManager.IncrementGamesCounter();
        PlayerPrefsManager.IncreaseTotalGemsCounter(_gemsOfThisSession);
        PlayerPrefsManager.ChangeBestScore(_gemsOfThisSession);
    }

    [ContextMenu("Clear PlayerPrefs")] //  temp
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}