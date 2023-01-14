using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuText : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_InputField nameInput;

    // Start is called before the first frame update
    void Start()
    {
        SetHighScoreText();
        RefillNameInputField();
    }

    // Set the high score name and text
    void SetHighScoreText()
    {
        highScoreText.text = $"Current High Score:\n{DataManager.Instance.highScoreName} - {DataManager.Instance.highScore}";
    }

    // If returning from the game scene, fill the player's name back into the input field
    void RefillNameInputField()
    {
        if (DataManager.Instance.playerName != null) nameInput.text = DataManager.Instance.playerName;
    }
}
