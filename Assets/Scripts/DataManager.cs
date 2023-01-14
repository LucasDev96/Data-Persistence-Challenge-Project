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
        string name;
        int points;
    }

    
}
