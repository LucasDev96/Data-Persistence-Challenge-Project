using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameButtons : MainMenuButtons
{
    public void GoToMainMenu()
    {
        DataManager.Instance.SaveHighScoreToFile();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
