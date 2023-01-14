using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName { get; set; }
    public string highScoreName { get; set; }
    public int playerScore { get; set; }
    public int highScore { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (highScoreName == null) highScoreName = "name";

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Class to store and save the player data for use with json file
    [Serializable]
    public class PlayerData
    {
        public string name;
        public int points;
    }

    // Use the PlayerData class to save the highest player score and name to a json file
    public void SaveHighScoreToFile()
    {
        PlayerData data = new PlayerData();
        data.name = highScoreName;
        data.points = highScore;

        string json = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/HighScoreData.json", json);
    }

    // Read from the json save file, if it exists, and set the appropriate variables
    public void LoadPlayerDataFromSaveFile()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/HighScoreData.json"))
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(Application.persistentDataPath + "/HighScoreData.json");

            highScoreName = data.name;
            highScore = data.points;
        }
    }
}
