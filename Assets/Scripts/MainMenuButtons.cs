using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public TMP_InputField nameInput;

    // Go to the main game after pressing the start button
    public void StartGame()
    {
        AssignPlayerName();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    // Use the text input field to assign the name in the DataManager
    private void AssignPlayerName()
    {
        if (nameInput.text.Length < 1)
        {
            DataManager.Instance.playerName = "name";
        }
        else
        {
            DataManager.Instance.playerName = nameInput.text;
        }
    }

    // Exit the application, or play mode if in the editor
    public void ExitGame()
    {
        DataManager.Instance.SaveHighScoreToFile();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Reset the scores and overwrite the high score file, reloading the scene so everything updates properly
    public void ResetScores()
    {
        DataManager.Instance.highScore = 0;
        DataManager.Instance.highScoreName = "name";

        DataManager.Instance.SaveHighScoreToFile();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
